using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CHIETAMIS.Documents;
using CHIETAMIS.Documents.Dtos;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace CHIETAMIS.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DocumentDownloadController : AbpController
    {
        private readonly DocumentsAppService _documentsAppService;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        private string FileServerBaseUrl =>
            (_configuration["DocumentStorage:FileServerBaseUrl"] ?? "https://ims.chieta.org.za:22742").TrimEnd('/');

        public DocumentDownloadController(
            DocumentsAppService documentsAppService,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _documentsAppService = documentsAppService;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        // ─────────────────────────────────────────────────────────────
        // UPLOAD ENDPOINT
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Upload a document and store it on the file server, then save/update the DB record.
        /// POST: /api/DocumentDownload/UploadDocument
        /// Form fields: file (required), entityId, userId, documentType, module
        /// </summary>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadDocument(
            [FromForm] IFormFile file,
            [FromForm] int entityId,
            [FromForm] int userId,
            [FromForm] string documentType,
            [FromForm] string module)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "No file provided." });

            try
            {
                // 1. Forward the file to the remote file server's /api/upload endpoint
                var client = _httpClientFactory.CreateClient("FileServer");
                string storedFileName;

                using (var content = new MultipartFormDataContent())
                using (var fileStream = file.OpenReadStream())
                {
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue(
                            string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType);

                    content.Add(streamContent, "file", file.FileName);

                    var uploadUrl = $"{FileServerBaseUrl}/api/upload";
                    var uploadResponse = await client.PostAsync(uploadUrl, content);

                    if (!uploadResponse.IsSuccessStatusCode)
                    {
                        var body = await uploadResponse.Content.ReadAsStringAsync();
                        Logger.Error($"[UploadDocument] File server rejected upload. Status={uploadResponse.StatusCode}, Body={body}");
                        return StatusCode((int)uploadResponse.StatusCode,
                            new { error = "File server rejected the upload.", detail = body });
                    }

                    var responseBody = await uploadResponse.Content.ReadAsStringAsync();
                    // The existing upload endpoint returns the uniquefilename as a plain JSON string
                    // e.g.: "\"guid_filename.pdf\"" or wrapped in ABP envelope {"result":"..."}
                    try
                    {
                        var json = JToken.Parse(responseBody);
                        storedFileName = json.Type == JTokenType.String
                            ? json.Value<string>()
                            : json["result"]?.Value<string>() ?? json.Value<string>();
                    }
                    catch
                    {
                        storedFileName = responseBody.Trim('"', ' ', '\r', '\n');
                    }
                }

                if (string.IsNullOrWhiteSpace(storedFileName))
                    return StatusCode(502, new { error = "File server returned an empty filename." });

                // 2. Save / update the document record in the database
                var docDto = new DocumentDto
                {
                    entityid      = entityId,
                    newfilename   = storedFileName,
                    filename      = file.FileName,
                    size          = file.Length.ToString(),
                    type          = file.ContentType,
                    documenttype  = documentType,
                    module        = module,
                    lastmodifieddate = DateTime.UtcNow.ToString("o"),
                    DateCreated   = DateTime.Now,
                    UserId        = userId
                };

                await _documentsAppService.FileUpload(docDto);

                // 3. Return the stored metadata + download URL
                var serverRoot = _configuration["App:ServerRootAddress"]?.TrimEnd('/');
                return Ok(new
                {
                    storedFileName,
                    originalFileName = file.FileName,
                    fileServerUrl    = BuildFileUrl(storedFileName),
                    downloadUrl      = $"{serverRoot}/api/DocumentDownload/DownloadById",
                    entityId,
                    userId,
                    documentType,
                    module,
                    fileSize         = file.Length
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"[UploadDocument] entityId={entityId}, documentType={documentType}", ex);
                return StatusCode(500, new { error = "An error occurred while uploading the file.", detail = ex.Message });
            }
        }

        // ─────────────────────────────────────────────────────────────
        // DOWNLOAD ENDPOINTS
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Download a document by its database ID.
        /// GET: /api/DocumentDownload/DownloadById?documentId=5
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DownloadById([FromQuery] int documentId)
        {
            try
            {
                var doc = await _documentsAppService.GetDocumentRecordById(documentId);
                return await ProxyFileAsync(doc.newfilename, doc.filename, doc.type);
            }
            catch (UserFriendlyException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                Logger.Error($"[DownloadById] documentId={documentId}", ex);
                return StatusCode(500, new { error = "An error occurred while downloading the file." });
            }
        }

        /// <summary>
        /// Download by entityId + documentType, with optional module and userId filters.
        /// GET: /api/DocumentDownload/DownloadByEntity?entityId=123&documentType=Invoice&userId=456&module=Grant
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DownloadByEntity(
            [FromQuery] int entityId,
            [FromQuery] string documentType,
            [FromQuery] string module = null,
            [FromQuery] int? userId = null)
        {
            try
            {
                var doc = await _documentsAppService.GetDocumentRecord(new DownloadDocumentRequestDto
                {
                    EntityId = entityId,
                    DocumentType = documentType,
                    Module = module,
                    UserId = userId
                });
                return await ProxyFileAsync(doc.newfilename, doc.filename, doc.type);
            }
            catch (UserFriendlyException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                Logger.Error($"[DownloadByEntity] entityId={entityId}, documentType={documentType}", ex);
                return StatusCode(500, new { error = "An error occurred while downloading the file." });
            }
        }

        // ─────────────────────────────────────────────────────────────
        // LIST / METADATA ENDPOINTS
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// List documents for a user (metadata + download URL). Supports pagination.
        /// GET: /api/DocumentDownload/GetUserDocuments?userId=456&documentType=Invoice&module=Grant&maxResultCount=50&skipCount=0
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetUserDocuments(
            [FromQuery] int userId,
            [FromQuery] string documentType = null,
            [FromQuery] string module = null,
            [FromQuery] int? entityId = null,
            [FromQuery] int maxResultCount = 100,
            [FromQuery] int skipCount = 0)
        {
            try
            {
                var result = await _documentsAppService.GetUserDocuments(new GetUserDocumentsRequestDto
                {
                    UserId = userId,
                    DocumentType = documentType,
                    Module = module,
                    EntityId = entityId,
                    MaxResultCount = maxResultCount,
                    SkipCount = skipCount
                });

                EnrichWithDownloadUrls(result.Items);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error("[GetUserDocuments]", ex);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// List all documents for an entity, optionally filtered by userId.
        /// GET: /api/DocumentDownload/GetByEntityId?entityId=123&userId=456
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetByEntityId(
            [FromQuery] int entityId,
            [FromQuery] int? userId = null)
        {
            try
            {
                var result = await _documentsAppService.GetDocumentsByEntityId(entityId, userId);
                EnrichWithDownloadUrls(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error($"[GetByEntityId] entityId={entityId}", ex);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// List documents by document type, with optional userId and module filters.
        /// GET: /api/DocumentDownload/GetByType?documentType=Invoice&userId=456&module=Grant
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetByType(
            [FromQuery] string documentType,
            [FromQuery] int? userId = null,
            [FromQuery] string module = null)
        {
            try
            {
                var result = await _documentsAppService.GetDocumentsByType(documentType, userId, module);
                EnrichWithDownloadUrls(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error($"[GetByType] documentType={documentType}", ex);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ─────────────────────────────────────────────────────────────
        // DIAGNOSTIC ENDPOINT
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Checks whether the remote file server can serve a document without downloading it.
        /// GET: /api/DocumentDownload/DiagnoseFile?documentId=4963
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> DiagnoseFile([FromQuery] int documentId)
        {
            try
            {
                var doc = await _documentsAppService.GetDocumentRecordById(documentId);
                var url = BuildFileUrl(doc.newfilename);

                var client = _httpClientFactory.CreateClient("FileServer");
                HttpResponseMessage response;
                try
                {
                    response = await client.SendAsync(
                        new HttpRequestMessage(HttpMethod.Head, url));
                }
                catch (Exception ex)
                {
                    return StatusCode(502, new { error = "Could not reach file server.", detail = ex.Message, url });
                }

                return Ok(new
                {
                    documentId,
                    storedFileName = doc.newfilename,
                    originalFileName = doc.filename,
                    fileServerUrl = url,
                    statusCode = (int)response.StatusCode,
                    accessible = response.IsSuccessStatusCode,
                    contentType = response.Content.Headers.ContentType?.ToString(),
                    contentLength = response.Content.Headers.ContentLength
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ─────────────────────────────────────────────────────────────
        // PRIVATE HELPERS
        // ─────────────────────────────────────────────────────────────

        private string BuildFileUrl(string storedFileName) =>
            $"{FileServerBaseUrl}/Files/{Uri.EscapeDataString(storedFileName)}";

        /// <summary>
        /// Fetches the file from the remote file server and streams it to the caller.
        /// </summary>
        private async Task<IActionResult> ProxyFileAsync(string storedFileName, string originalFileName, string mimeType)
        {
            if (string.IsNullOrWhiteSpace(storedFileName))
                return BadRequest(new { error = "Document record has no stored filename." });

            var url = BuildFileUrl(storedFileName);
            var client = _httpClientFactory.CreateClient("FileServer");

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            }
            catch (Exception ex)
            {
                Logger.Error($"[DocumentDownload] File server unreachable for '{storedFileName}': {ex.Message}");
                return StatusCode(502, new { error = "File server is unreachable.", detail = ex.Message });
            }

            if (!response.IsSuccessStatusCode)
            {
                Logger.Warn($"[DocumentDownload] File server returned {(int)response.StatusCode} for '{storedFileName}' — URL: {url}");
                return NotFound(new
                {
                    error = $"File '{originalFileName}' was not found on the file server.",
                    storedFileName,
                    fileServerUrl = url,
                    statusCode = (int)response.StatusCode
                });
            }

            Logger.Info($"[DocumentDownload] Streaming '{originalFileName}' from {url}");

            var resolvedMime = ResolveMimeType(
                mimeType,
                response.Content.Headers.ContentType?.MediaType,
                originalFileName);

            var stream = await response.Content.ReadAsStreamAsync();
            return File(stream, resolvedMime, originalFileName);
        }
        private void EnrichWithDownloadUrls(IEnumerable<DocumentDownloadInfoDto> items)
        {
            var serverRoot = _configuration["App:ServerRootAddress"]?.TrimEnd('/');
            foreach (var item in items)
                item.DownloadUrl = $"{serverRoot}/api/DocumentDownload/DownloadById?documentId={item.Id}";
        }

        private static string ResolveMimeType(string storedType, string serverContentType, string fileName)
        {
            if (!string.IsNullOrWhiteSpace(storedType) && storedType != "application/octet-stream")
                return storedType;

            if (!string.IsNullOrWhiteSpace(serverContentType) && serverContentType != "application/octet-stream")
                return serverContentType;

            var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
            return ext switch
            {
                ".pdf"  => "application/pdf",
                ".doc"  => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls"  => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".png"  => "image/png",
                ".jpg"  => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".zip"  => "application/zip",
                _       => "application/octet-stream"
            };
        }
    }
}

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
using Microsoft.AspNetCore.Http;

namespace CHIETAMIS.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DocumentDownloadController : AbpController
    {
        private readonly DocumentsAppService _documentsAppService;
        private readonly IConfiguration _configuration;

        public DocumentDownloadController(
            DocumentsAppService documentsAppService,
            IConfiguration configuration)
        {
            _documentsAppService = documentsAppService;
            _configuration = configuration;
        }

        // ─────────────────────────────────────────────────────────────
        // UPLOAD ENDPOINT
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Upload a document and save it directly to the configured storage path, then save/update the DB record.
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
                var filesPath = _configuration["DocumentStorage:FilesPath"];
                if (string.IsNullOrWhiteSpace(filesPath))
                    return StatusCode(500, new { error = "DocumentStorage:FilesPath is not configured." });

                Directory.CreateDirectory(filesPath);

                // Generate a unique filename using the same convention as the file server
                var storedFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var fullPath = Path.Combine(filesPath, storedFileName);

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    await file.CopyToAsync(fs);

                Logger.Info($"[UploadDocument] Saved '{file.FileName}' to '{fullPath}'");

                // Save / update the document record in the database
                var docDto = new DocumentDto
                {
                    entityid         = entityId,
                    newfilename      = storedFileName,
                    filename         = file.FileName,
                    size             = file.Length.ToString(),
                    type             = file.ContentType,
                    documenttype     = documentType,
                    module           = module,
                    lastmodifieddate = DateTime.UtcNow.ToString("o"),
                    DateCreated      = DateTime.Now,
                    UserId           = userId
                };

                await _documentsAppService.FileUpload(docDto);

                var serverRoot = _configuration["App:ServerRootAddress"]?.TrimEnd('/');
                return Ok(new
                {
                    storedFileName,
                    originalFileName = file.FileName,
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
                return ServeFileFromDisk(doc.newfilename, doc.filename, doc.type);
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
                return ServeFileFromDisk(doc.newfilename, doc.filename, doc.type);
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
                var candidatePaths = GetCandidateFilePaths(doc.newfilename).ToList();
                var probeResults = candidatePaths.Select(p => new
                {
                    path   = p,
                    exists = System.IO.File.Exists(p)
                }).ToList();

                return Ok(new
                {
                    documentId,
                    storedFileName   = doc.newfilename,
                    originalFileName = doc.filename,
                    probeResults
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

        /// <summary>
        /// Returns the ordered set of absolute filesystem paths to probe for a stored file.
        /// Checks DocumentStorage:FilesPath first, then every entry in DocumentStorage:FallbackPaths.
        /// Handles the case where storedFileName was recorded as a full path or URL by
        /// always resolving to the bare filename before joining with each directory.
        /// </summary>
        private IEnumerable<string> GetCandidateFilePaths(string storedFileName)
        {
            if (string.IsNullOrWhiteSpace(storedFileName))
                yield break;

            // Normalise: regardless of whether the DB has a bare name, a full path, or a URL,
            // we only ever store/retrieve by the leaf filename.
            var bareName = Path.GetFileName(storedFileName);
            if (string.IsNullOrWhiteSpace(bareName))
                bareName = storedFileName;

            var primary = _configuration["DocumentStorage:FilesPath"];
            var fallbacks = _configuration.GetSection("DocumentStorage:FallbackPaths")
                                          .Get<string[]>() ?? Array.Empty<string>();

            foreach (var dir in new[] { primary }.Concat(fallbacks)
                                                  .Where(d => !string.IsNullOrWhiteSpace(d))
                                                  .Distinct(StringComparer.OrdinalIgnoreCase))
            {
                yield return Path.Combine(dir, bareName);
            }
        }

        /// <summary>
        /// Locates the file on the local filesystem across all configured storage paths
        /// and streams it directly to the caller. This approach works for both old files
        /// (stored under Files/) and new files (stored under Attachment/ or any future path)
        /// without relying on the remote file-server HTTP endpoint.
        /// </summary>
        private IActionResult ServeFileFromDisk(string storedFileName, string originalFileName, string mimeType)
        {
            if (string.IsNullOrWhiteSpace(storedFileName))
                return BadRequest(new { error = "Document record has no stored filename." });

            foreach (var path in GetCandidateFilePaths(storedFileName))
            {
                if (!System.IO.File.Exists(path))
                {
                    Logger.Info($"[DocumentDownload] Not found at '{path}'");
                    continue;
                }

                Logger.Info($"[DocumentDownload] Serving '{originalFileName}' from '{path}'");
                var resolvedMime = ResolveMimeType(mimeType, null, originalFileName);
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(stream, resolvedMime, originalFileName);
            }

            var tried = string.Join(", ", GetCandidateFilePaths(storedFileName));
            Logger.Error($"[DocumentDownload] File not found on disk for '{storedFileName}'. Paths tried: {tried}");
            return NotFound(new
            {
                error        = $"File '{originalFileName}' was not found.",
                storedFileName,
                statusCode   = 404
            });
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

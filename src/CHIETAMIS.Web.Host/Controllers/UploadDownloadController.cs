using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using CHIETAMIS.Controllers;

namespace CHIETAMIS.Web.Host.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api")]
    public class UploadDownloadController : CHIETAMISControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private string FileServerBaseUrl =>
            (_configuration["DocumentStorage:FileServerBaseUrl"] ?? "https://ims.chieta.org.za:22742").TrimEnd('/');

        public UploadDownloadController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "No file provided." });

            var client = _httpClientFactory.CreateClient("FileServer");

            using var content = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();

            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue(
                    string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType);
            content.Add(streamContent, "file", file.FileName);

            HttpResponseMessage uploadResponse;
            try
            {
                uploadResponse = await client.PostAsync($"{FileServerBaseUrl}/api/upload", content);
            }
            catch (Exception ex)
            {
                return StatusCode(502, new { error = "File server is unreachable.", detail = ex.Message });
            }

            var responseBody = await uploadResponse.Content.ReadAsStringAsync();

            if (!uploadResponse.IsSuccessStatusCode)
                return StatusCode((int)uploadResponse.StatusCode,
                    new { error = "File server rejected the upload.", detail = responseBody });

            // Parse the stored filename out of the ABP envelope or plain string response
            string storedFileName;
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

            if (string.IsNullOrWhiteSpace(storedFileName))
                return StatusCode(502, new { error = "File server returned an empty filename." });

            // Return in the same ABP envelope shape callers already expect
            return Ok(storedFileName);
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<ActionResult> Download([FromQuery] string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return BadRequest(new { error = "filename is required." });

            var client = _httpClientFactory.CreateClient("FileServer");
            var url = $"{FileServerBaseUrl}/Files/{Uri.EscapeDataString(filename)}";

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            }
            catch (Exception ex)
            {
                return StatusCode(502, new { error = "File server is unreachable.", detail = ex.Message });
            }

            if (!response.IsSuccessStatusCode)
                return NotFound(new { error = $"File '{filename}' was not found on the file server.", statusCode = (int)response.StatusCode });

            var mimeType = GetContentType(filename);
            var stream = await response.Content.ReadAsStreamAsync();
            return File(stream, mimeType, filename);
        }

        [HttpGet]
        [Route("files")]
        public async Task<IActionResult> Files()
        {
            var client = _httpClientFactory.CreateClient("FileServer");

            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"{FileServerBaseUrl}/api/files");
            }
            catch (Exception ex)
            {
                return StatusCode(502, new { error = "File server is unreachable.", detail = ex.Message });
            }

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { error = "Could not retrieve file list from file server." });

            var body = await response.Content.ReadAsStringAsync();

            // Parse the file list — strip full paths, return just filenames
            try
            {
                var json = JToken.Parse(body);
                var raw = json.Type == JTokenType.Array
                    ? json
                    : json["result"] as JArray ?? new JArray();

                var names = raw
                    .Select(t => Path.GetFileName(t.Value<string>()))
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList();

                return Ok(names);
            }
            catch
            {
                return Ok(body);
            }
        }

        private static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
                contentType = "application/octet-stream";
            return contentType;
        }
    }
}

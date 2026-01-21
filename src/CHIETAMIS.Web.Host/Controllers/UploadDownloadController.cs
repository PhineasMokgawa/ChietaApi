using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Zero;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Net.Http.Headers;
using CHIETAMIS.Controllers;

namespace CHIETAMIS.Web.Host.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api")]
    //[DependsOn(typeof(AbpZeroCommonModule))]
    public class UploadDownloadController : CHIETAMISControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public UploadDownloadController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string uniquefilename = null;
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            if (file.Length > 0)
            {
                uniquefilename = Guid.NewGuid().ToString() + "_" + file.FileName.Trim();
                var filePath = Path.Combine(uploads, uniquefilename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok(uniquefilename);
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<ActionResult> Download([FromQuery] string filename)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
            var filePath = Path.Combine(uploads, filename);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), filename);
        }

        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}

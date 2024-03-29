﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BugTracker.Controllers
{
    [ApiController]
    [Authorize]
    [Route("bugtracker/file")]
    public class FileController : ControllerBase
    {
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "fileName" })]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/StaticFiles/{fileName}";

            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            var contentProvider = new FileExtensionContentTypeProvider();
            if (!contentProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            };

            var fileContent = System.IO.File.ReadAllBytes(filePath);

            //return $"data:{contentType};base64,{Convert.ToBase64String(fileContent)}";

            return File(fileContent, contentType, fileName);
        }

        [HttpPost]
        public ActionResult UploadFile([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/StaticFiles/{fileName}";

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult DeleteFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/StaticFiles/{fileName}";

            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            System.IO.File.Delete(filePath);

            return Ok();
        }       
    }
}
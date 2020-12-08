using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JLForecasterWeb.Models;
using JLForecasterWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JLForecasterWeb.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FileLoadController : Controller
    {
        private readonly ILogger<FileLoadController> _logger;
        private readonly FileLoadService _fileLoadService;
        private readonly FileService _fileService;

        public FileLoadController(ILogger<FileLoadController> logger, FileLoadService fileLoadService, FileService fileService)
        {
            _logger = logger;
            _fileLoadService = fileLoadService;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FileLoadedView")]
        public async Task<IActionResult> FileLoadedView(IFormFile pocFile)
        {

            //var fileType = "Geco";
            //long size = pocFile.Length;
            //var basePath = Path.Combine(_webHostEnvironment.WebRootPath + "\\Files\\" + fileType);
            //var filePath = Path.Combine(basePath, pocFile.FileName);
            //_fileService.FileSize = size.ToString();
            //if (size > 0)
            //{
            //    _fileService.status = "Fileloaded";
            //    _fileService.FileContent = pocFile;
            //    _fileService.FileName = filePath;
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await pocFile.CopyToAsync(stream);
            //    }
            //}
            //else
            //{
            //    _fileService.status = "File load failed.";
            //    // setup an error screen return
            //}
            return View(_fileService);
        }
    }
}

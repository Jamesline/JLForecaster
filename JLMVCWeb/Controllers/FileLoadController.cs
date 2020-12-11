using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JLForecasterWeb.Models;
using JLForecasterWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public async Task<IActionResult> FileLoadedView(FileLoadModel fileLoadedModel)
        {
            var pocFile = fileLoadedModel.FileLoaded;
            long size = pocFile.Length;
            var FileName = pocFile.FileName;
            //FileLoadService fileloadservice = new FileLoadService(_logger,)
            var result = await _fileLoadService.AZFileStorer(fileLoadedModel.FileLoaded, fileLoadedModel.FileType);
            _fileService.FileName = FileName;
            _fileService.FileSize = size.ToString();
            return View(_fileService);
        }
    }
}

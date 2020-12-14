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
        private readonly FileServiceModel _fileServiceModel;


        public FileLoadController(ILogger<FileLoadController> logger, FileLoadService fileLoadService, FileServiceModel fileServiceModel)
        {
            _logger = logger;
            _fileLoadService = fileLoadService;
            _fileServiceModel = fileServiceModel;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("FileLoadedView")]
        public async Task<IActionResult> FileLoadedView(FileLoadModel fileLoadedModel)
        {
            var loadedFile = fileLoadedModel.FileContentLoaded;
            if (loadedFile == null)
            {
                return View(ErrorMessage());
            }
            long fileSize = loadedFile.Length;
            if (fileSize < 1)
            {
                return View(ErrorMessage());
            }

            var fileName = loadedFile.FileName;
            var result = await _fileLoadService.AZFileStorer(loadedFile, fileLoadedModel.FileTypeExt);
            if (result =="Success")
            {
                _fileServiceModel.fileName = fileName;
                _fileServiceModel.fileSize = fileSize.ToString();
                _fileServiceModel.status = "File Loaded";
                _fileServiceModel.fileExt = fileLoadedModel.FileTypeExt; 
            }
            else
            {
                return View(ErrorMessage());
            }

            return View(_fileServiceModel);
            //return View();
        }
        
        private FileServiceModel ErrorMessage()
        {
            _fileServiceModel.fileName = "None";
            _fileServiceModel.fileSize = "0";
            _fileServiceModel.status = "File load failed";
            _fileServiceModel.fileExt = "None";
            return _fileServiceModel;
        }
    }
}

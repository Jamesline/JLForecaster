﻿using JLMVCWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using JLForecasterWeb.Services;

namespace JLForecasterWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileLoadService _fileService;

        public HomeController(ILogger<HomeController> logger, FileLoadService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            //IFileService financeService = new FileService();
            //financeService.AZFileStore("Geco2.xlsx", @"C:\Data\Geco\Nov 1 BU 1.xlsx", "geco");
            return View();
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Example()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

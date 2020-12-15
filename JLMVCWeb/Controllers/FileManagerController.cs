using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JLForecasterWeb.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JLForecasterWeb.Controllers
{
    [ViewLayout("_FileManagerLayout")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

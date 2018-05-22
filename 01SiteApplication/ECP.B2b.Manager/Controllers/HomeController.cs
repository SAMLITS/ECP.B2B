using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.Manager.Models;
using Microsoft.AspNetCore.Authorization;
using ECP.Util.Common;

namespace ECP.B2b.Manager.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IEnumerable<string> Get()
        {
            //string url  = ApplicationConfig.ClientFileUploadDomain;  前台上传地址
            //string url  = ApplicationConfig.ManagerFileUploadDomain; 后台上传地址

            return new string[] { "value1", "value2" };
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

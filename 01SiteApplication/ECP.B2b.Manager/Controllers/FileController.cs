using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using ECP.Util.Common;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.Util.HtmlHelper;
using ECP.Util.HtmlHelper.Filter;
using static ECP.Util.HtmlHelper.FileUploadExtend;

namespace ECP.B2b.Manager.Controllers
{
    [LoginManagerAuthorityFilter]
    [Route("file")]
    public class FileController : Controller
    {
        public CurrentUserEntity CurrentUser => HttpContext.GetCurrentUser();
        private IHostingEnvironment hostingEnv;
        public FileController(IHostingEnvironment _hostingEnv)
        {
            hostingEnv = _hostingEnv;
        }

        [HttpGet("upload")]
        public IActionResult FileUpload(FileUploadFilterEntity fileUploadFilterEntity)
        {
            return View(fileUploadFilterEntity);
        }
        [HttpPost("upload")]
        public JsonResult FileUploadPost()
        {
            UploadResult uploadResult = HttpContext.UploadSingleFile(hostingEnv, CurrentUser.USER);
            return Json(uploadResult);
        }
    }
}
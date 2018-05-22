using ECP.Util.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace ECP.Util.HtmlHelper
{
    public static class FileUploadExtend
    {
        /// <summary>
        /// 每次只是上传一个文件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="hostingEnv"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UploadResult UploadSingleFile(this HttpContext httpContext, IHostingEnvironment hostingEnv, string user)
        {
            DateTime dateTime = DateTime.Now;
            string UploadPath = hostingEnv.WebRootPath;
            string FiexdPath = Path.Combine(@"\Uploads", httpContext.Request.Form["UploadPath"], $@"{dateTime.ToString("yyyy")}" + $@"\{dateTime.ToString("MM")}" + $@"\{dateTime.ToString("dd")}") ;
            //上传完整目录
            string FullPath = UploadPath + FiexdPath;

            var file = httpContext.Request.Form.Files[0]; 
            var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            FileHelper.CreateDirectory(FullPath);

            filename = $@"{dateTime.ToString("yyyyMMddHHmmss") + "_" + user + "_" + filename}";
             
            using (FileStream fs = System.IO.File.Create(Path.Combine(FullPath, filename)))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            return new UploadResult
            {
                code = 1,
                uploadPath = Path.Combine(FiexdPath, filename)
            };
        }


        public class UploadResult
        {
            public int code { get; set; }
            public string uploadPath { get; set; }
        }

    }
}
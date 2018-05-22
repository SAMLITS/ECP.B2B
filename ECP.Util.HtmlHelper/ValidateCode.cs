using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ECP.Util.Common;
using Microsoft.AspNetCore.Http;

namespace ECP.Util.HtmlHelper
{
    public static class ValidateCode
    {
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="context"></param>
        /// <returns>bytes=图片字节  cType=图片类型  </returns>
        public static (byte[] bytes, string cType) ValidateCodeBuild(this HttpContext context)
        {
            ValidateCodeServices _vierificationCodeServices = new ValidateCodeServices();
            System.IO.MemoryStream ms = _vierificationCodeServices.Create(out string code);
            context.Session.SetString("LoginValidateCode", code);
            context.Response.Body.Dispose();
            return (ms.ToArray(), @"image/png");
        }
    }
}

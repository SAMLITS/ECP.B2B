using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.Jwt.LTS.Extend
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// 通过cookie 填充请求头权限信息 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCookieHeaderMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CookieHeaderMiddleware>();
        }
    }
}

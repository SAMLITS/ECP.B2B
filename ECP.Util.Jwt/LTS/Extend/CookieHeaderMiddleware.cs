using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.Util.Jwt.LTS.Extend
{
    public class CookieHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        public IAuthenticationSchemeProvider Schemes { get; set; }
        public CookieHeaderMiddleware(
            RequestDelegate next, IAuthenticationSchemeProvider schemes)
        {
            _next = next;
            Schemes = schemes;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                string cookie = context.Request.Cookies["userToken"];
                context.Request.Headers.Add("Authorization", "Bearer " + cookie);

                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result?.Principal != null)
                    {
                        context.User = result.Principal;
                    }
                }
            }
            await _next(context);
        }
    }
}

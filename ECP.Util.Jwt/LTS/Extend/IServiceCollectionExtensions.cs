using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECP.Util.Jwt.LTS.Extend
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSiteRegisterJwt(this IServiceCollection services,string Issuer, string audience)
        {
            // 从文件读取密钥
            JWTTokenOptions _tokenOptions = new JWTTokenOptions();
            string keyDir = PlatformServices.Default.Application.ApplicationBasePath;
            if (RSAUtils.TryGetKeyParameters(keyDir, false, out RSAParameters keyparams) == false)
            {
                _tokenOptions.Key = default(RsaSecurityKey);
            }
            else
            {
                _tokenOptions.Key = new RsaSecurityKey(keyparams);
            }
            _tokenOptions.Issuer = Issuer; // 设置签发者
            _tokenOptions.Audience = audience; // 设置签收者，也就是这个应用服务器的名称
            _tokenOptions.Credentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    //.AddRequirements(new ValidJtiRequirement()) // 添加上面的验证要求
                    .Build());
            });
            // 注册验证要求的处理器，可通过这种方式对同一种要求添加多种验证
            //services.AddSingleton<IAuthorizationHandler, ValidJtiHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = _tokenOptions.Key,
                    ValidAudience = _tokenOptions.Audience,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidateLifetime = true
                };
            });
        }

    }
}

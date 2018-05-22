using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System.Security.Cryptography;
using ECP.Util.Jwt.LTS;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Autofac;

namespace ECP.B2B.Issuer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 从文件读取密钥
            string keyDir = PlatformServices.Default.Application.ApplicationBasePath;
            if (RSAUtils.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAUtils.GenerateAndSaveKey(keyDir);
            }
            JWTTokenOptions _tokenOptions = new JWTTokenOptions();
            _tokenOptions.Key = new RsaSecurityKey(keyParams);
            _tokenOptions.Issuer = "EcpB2bIssuer"; // 签发者名称
            _tokenOptions.Credentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);
            // 添加到 IoC 容器  有可能报错  改为不是单例
            services.AddSingleton(_tokenOptions);

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

            services.AddDataProtection(options =>
            {
                options.ApplicationDiscriminator = "localhost";
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Domain = "localhost";
                options.Cookie.Name = ".AspNetCore.Cookies";
            });

            services.AddMvc();


            return
                Util.AutofacIoc.AutofacHelp.AutofacProviderBuilderCore(
                    services,
                    ApplicationContainer,
                    new B2b.ClientRegisterModuleIoc.GrpcClientModule()
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}

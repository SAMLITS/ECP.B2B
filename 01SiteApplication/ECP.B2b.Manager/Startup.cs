using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Http.Features;
using ECP.Util.Jwt.LTS;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using ECP.Util.Jwt.LTS.Extend;
using ECP.B2b.Manager.Models;
using ECP.Util.Common;
using ECP_B2B_API_SDK;
using ECP_B2B_API_SDK.Extensions;
using ECP.Util.HtmlHelper;

namespace ECP.B2b.Manager
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
            //客户端站点注册JWT 
            services.AddSiteRegisterJwt(ApplicationConfig.Issuer, ApplicationConfig.ManagerAudience);

            //初始化配置
            AppContextConfigInfo.InitAppConfigInfo(Configuration);

            //ECP B2B API 初始化配置
            ApiSdkClient_Config.ServerAppDomain = ApplicationConfig.Ecp_b2b_sdk_api_domain;
            ApiSdkClient_Config.ClientSecretKey = () => ApplicationConfig.Ecp_b2b_sdk_api_secretKey;
            ApiSdkClient_Config.EcpB2bOpenDomainAddress = () => ApplicationConfig.Ecp_open_domain_address;
            ApiSdkClient_Config.GetUserUniqueFunc = () =>
            {
                var userInfo = ECP_B2B_API_SDK.Extensions.HttpContext.Current.GetCurrentUser();
                return userInfo == null ? -100 : userInfo.ID;
            };

            //添加Session功能
            services.AddSession();

            //设置文件上传的大小限制为10 MB。
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 10000000;
            });

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddMvc().AddJsonOptions(p => p.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());


            services.AddHttpContextAccessor();

            return
                Util.AutofacIoc.AutofacHelp.AutofacProviderBuilderCore(
                    services,
                    ApplicationContainer,
                    new ClientRegisterModuleIoc.GrpcClientModule()
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCookieHeaderMiddleware();
            app.UseStaticHttpContext();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //注入session 
            app.UseSession();

            app.UseMvc(routes =>
            { 
                routes.MapAreaRoute(
                  name: "systemArea",
                  areaName: "System",
                  template: "System/{controller=QueryOrder}/{action=Queryorder}"
                );  
            });

        }
    }
}
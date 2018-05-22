using ECP.B2b.DAL;
using ECP.B2b.DAL.Interface;
using ECP.B2b.EF;
using ECP.B2b.Main.GrpcService;
using ECP.B2b.Service;
using ECP.B2b.Service.Interface; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.Util.Common;
using Grpc.Core;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.DAL.Basic;
using ECP.B2b.DAL.Interface.Basic;
using ECP.B2b.Service.Basic;
using ECP.B2b.Service.Interface.Basic;
using ECP.B2b.DAL.System;
using ECP.B2b.DAL.Interface.System;
using ECP.B2b.Service.Interface.System;
using ECP.B2b.Service.System; 
using ECP.B2b.DbModel.Sys; 
namespace ECP.B2b.ServerRegisterModuleIoc
{
    /// <summary>
    /// Grpc Server服务端启动时进行的注入
    /// </summary>
    public class GrpcServerModule
    {
        public static IServiceProvider ServiceDIRegister(IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IBaseTransService), typeof(BaseTransService));
            services.AddTransient(typeof(IBaseDal<>), typeof(BaseDal<>));
            services.AddTransient(typeof(IBaseTransDal), typeof(BaseTransDal));
            services.AddTransient<DbContext, B2bDbContext>();

            services.AddTransient<IB2B_USER_Dal, B2B_USER_Dal>();
            services.AddTransient<IB2B_USER_Service, B2B_USER_Service>();

            services.AddTransient<IB2B_LOOKUP_VALUES_Dal, B2B_LOOKUP_VALUES_Dal>();
            services.AddTransient<IB2B_LOOKUP_VALUES_Service, B2B_LOOKUP_VALUES_Service>();

            services.AddTransient<IB2B_USER_MENU_Dal, B2B_USER_MENU_Dal>();
            services.AddTransient<IB2B_USER_MENU_Service, B2B_USER_MENU_Service>();

            services.AddTransient<IB2B_MESSAGES_Dal, B2B_MESSAGES_Dal>();
            services.AddTransient<IB2B_MESSAGES_Service, B2B_MESSAGES_Service>(); 

            services.AddTransient<IB2B_USER_FUNCTION_Dal, B2B_USER_FUNCTION_Dal>();
            services.AddTransient<IB2B_USER_FUNCTION_Service, B2B_USER_FUNCTION_Service>(); 

            //注意： 代理服务不再需要注册
            //services.AddTransient<MenuProxy.IMenuProxyBase, MenuProxyService>();
            //services.AddTransient<LookUpValuesAllProxy.ILookUpValuesAllProxyBase, LookUpValuesAllProxyService>();
            //services.AddTransient<UserProxy.IUserProxyBase, UserProxyService>();
            return services.BuildServiceProvider();
        }

    }
}

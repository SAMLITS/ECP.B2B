using ECP.Util.Common;
using ECP.Util.ConfigDc.GrpcClient;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using ECP.B2b.Service.Interface;
using ECP.B2b.Service;
using ECP.B2b.DAL.Interface;
using ECP.B2b.DAL;
using ECP.B2b.EF;
using ECP.B2b.Main.GrpcService;
using ECP.B2b.Service.Interface.Basic;
using ECP.B2b.Main.GrpcProxy.Basic;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcService.System;
using ECP.B2b.Service.Interface.System;  
using ECP.Util.Common.Extensions;
using System.Collections.Generic;  
using ECP.Util.ConfigDc.Helper; 
using ECP.B2b.DBUtility; 

namespace ECP.B2b.Main.GrpcServer
{
    class Program
    {

        private static Logger logger = new Logger(typeof(Program));

        static void Main(string[] args)
        {
            ConfigDcConfig.GetServerName(out string serverName);
            Util.ConfigDc.ProtoProxy.ServerAddressReply serverAddressReply = ConfigDcUtilClientImp.GetServerAddress(serverName).Result;
            string Ip = serverAddressReply.ServerIp;
            int Port = serverAddressReply.ServerPort;


            Console.Title = "B2B监听服务";
            try
            {
                IServiceCollection services = new ServiceCollection();

                #region  EF Context注册
                services.AddDbContext<B2bDbContext>(options =>
                    options.UseSqlServer
                    (
                        ConfigDcUtilClientImp.GetDbConnectionConfig().GetAwaiter().GetResult()
                    )
                );
                ConsoleExtendsions.WriteLineSuccess("EF Context 注册成功... ");
                #endregion


                #region DI注入
                IServiceProvider serviceProvider = ServerRegisterModuleIoc.GrpcServerModule.ServiceDIRegister(services);
                ConsoleExtendsions.WriteLineSuccess("DI 注入成功... ");
                #endregion


                #region 视图重置
                string[] files = FileHelper.GetFiles("cfgViews");
                List<ViewEntity> viewEntityList = new List<ViewEntity>();
                Console.WriteLine();
                Console.WriteLine($"共检测到 {files.Length} 个视图需要重置，正在重置...");
                CreateInitView createInitView = new CreateInitView(serviceProvider.GetService<DbContext>());
                for (int i = 0; i < files.Length; i++)
                {
                    string fileContent = FileHelper.FileReadAllText(files[i]);
                    ViewEntity v = new ViewEntity(FileHelper.GetFileName(files[i]), fileContent);
                    createInitView.RunInitCreate(v);
                    ConsoleExtendsions.WriteLineSuccess($"{i + 1}. {v.viewName} 视图重置成功... ");
                }
                ConsoleExtendsions.WriteLineSuccess("重置数据库视图完成... ");
                Console.WriteLine();
                #endregion

                #region Oracle 配置
                DbStartConfig.DbOracleConnectionString = ConfigDcUtilClientImp.GetDbConnectionConfig("OracleDbConnectionString").GetAwaiter().GetResult();
                #endregion



                Server server = new Server()
                {
                    Ports = { new ServerPort(Ip, Port, ServerCredentials.Insecure) },
                    Services =
                {
                    MenuProxy.BindService(new MenuProxyService(()=>serviceProvider.GetService<IBaseService<B2B_MENU>>())),
                    LookUpValuesAllProxy.BindService(new LookUpValuesAllProxyService(()=>serviceProvider.GetService<IBaseService<B2B_LOOKUP_VALUES_ALL>>())),

                    LookUpValuesProxy.BindService(new LookUpValuesProxyService(()=>serviceProvider.GetService<IB2B_LOOKUP_VALUES_Service>())),

                    MessagesProxy.BindService(new MessagesProxyService(()=>serviceProvider.GetService<IB2B_MESSAGES_Service>())), 
                    UserProxy.BindService(new UserProxyService(()=>serviceProvider.GetService<IB2B_USER_Service>())),

                    UserMenuProxy.BindService(new UserMenuProxyService(()=>serviceProvider.GetService<IB2B_USER_MENU_Service>())),  
                MenuFunctionProxy.BindService(new MenuFunctionProxyService(()=>serviceProvider.GetService<IBaseService<B2B_MENU_FUNCTION>>())),
                UserFunctionProxy.BindService(new UserFunctionProxyService(()=>serviceProvider.GetService<IB2B_USER_FUNCTION_Service>())), 
                 
                    }
                };
                server.Start();

                ConsoleExtendsions.WriteLineSuccess("gRPC 服务启动成功，正在监听：port " + Port);
                Console.WriteLine("任意键退出...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();

            }
            catch (Exception ex)
            {
                ConsoleExtendsions.WriteLineError($"gRPC 服务监听启动失败！失败 port：{Port}！错误详细信息：{ex.GetBaseException().ToString()}");
                Console.WriteLine("任意键退出...");
                Console.ReadKey();
                logger.Error(ex: ex);
            }
        }
    }
}

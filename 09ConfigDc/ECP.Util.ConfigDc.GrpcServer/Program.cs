using ECP.Util.Common;
using ECP.Util.Common.Extensions;
using ECP.Util.ConfigDc.Helper;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ECP.Util.ConfigDc.GrpcServer
{ 
    /// <summary>
    /// 此RPC为全局配置中心，目前配置有服务发现与数据库相关配置，需要保证一直启动
    /// </summary>
    class Program
    {
        private static Logger logger = new Logger(typeof(Program));
         

        static void Main(string[] args)
        {
            ConfigDcConfig.GetDcAddress(out string Address, out string Ip, out int Port);

            Console.Title = "B2B服务中心";

            try
            {
                Server server = new Server()
                {
                    Ports = { new ServerPort(Ip, Port, ServerCredentials.Insecure) },
                    Services =
                {
                    ConfigDc.ProtoProxy.ConfigDcUtil.BindService(new ConfigDcUtilServiceImp())
                }
                };
                server.Start();

                ConsoleExtendsions.WriteLineSuccess("gRPC 服务启动成功，正在监听：port " + Port);
                Console.WriteLine("任意键退出...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
            catch(Exception ex)
            {
                ConsoleExtendsions.WriteLineError($"gRPC 服务监听启动失败！失败 port：{Port}！错误详细信息：{ex.GetBaseException().ToString()}");
                Console.WriteLine("任意键退出...");
                Console.ReadKey();
                logger.Error(ex:ex);
            }
        }
    }
}

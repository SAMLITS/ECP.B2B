using ECP.Util.Common;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECP.Util.Grpc
{
    public class GrpcServerHelp
    {
        private static Logger logger = new Logger(typeof(GrpcServerHelp));
        public static void StartRunServer(int port,Server.ServiceDefinitionCollection serviceDefinitionCollection)
        {
            try
            {
                //读取配置文件，进行Services注册



                Server server = new Server()
                {
                    Ports = { new ServerPort("127.0.0.1", port, ServerCredentials.Insecure) },
                    Services = {
                            
                    }
                }; 

                server.Start();


                Console.WriteLine("gRPC serverlistening on port " + port);
                Console.WriteLine("任意键退出...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
            catch(Exception ex)
            {
                logger.Error("Grpc Server启动失败：",ex);
                Console.WriteLine("启动失败！请查看错误日志！");
                Console.ReadKey();
            }
        }

    }
}

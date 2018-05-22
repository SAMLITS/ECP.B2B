using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECP.Util.ConfigDc.ProtoProxy;
using Grpc.Core;
using ECP.Util.Common;
using Newtonsoft.Json.Linq;
using ECP.Util.ConfigDc.Helper;

namespace ECP.Util.ConfigDc.GrpcServer
{
    public class ConfigDcUtilServiceImp: ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilBase
    {
        private Logger logger = new Logger(typeof(ConfigDcUtilServiceImp));

        /// <summary>
        /// 拿到数据库配置
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<DbConfigReply> GetDbConnectionConfig(DbConfigRequest request, ServerCallContext context)
        {  
            return Task.Run<DbConfigReply>(() =>
            {
                return new DbConfigReply()
                {
                    DbConfigVal = ReadConfigStatic.dbConfigObj.Value<string>(request.KeyName) ?? ""
                };
            });
            //一样
            //return Task.FromResult(new DbConfigReply() { DbConfigVal = ReadConfigStatic.dbConfigObj.Value<string>(request.KeyName) });
        }

        /// <summary>
        /// 拿取服务发现配置   先全名查找，再基础公共查找
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ServiceFindReply> GetGrpcServiceConfig(ServiceFindRequest request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                string config= ReadConfigStatic.serviceRegObj["ExtendService"].Value<string>(request.ServiceType+"_"+ request.ServiceName);
                if (string.IsNullOrEmpty(config))
                {
                    config = ReadConfigStatic.serviceRegObj["BaseService"].Value<string>(request.ServiceName);
                }

                config = config ?? "";

                 
                if (!string.IsNullOrEmpty(config))
                {
                    ConfigDcConfig.GetServerAddress(config, out config);
                }

                return new ServiceFindReply() { ServiceAddress = config };
            });
             
        }

        /// <summary>
        /// 获取全局配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ApplicationConfigReply> GetApplicationConfig(ApplicationConfigRequest request, ServerCallContext context)
        {
            return Task.Run<ApplicationConfigReply>(() =>
            {
                
                return new ApplicationConfigReply()
                {
                    ConfigValue = ReadConfigStatic.applicationConfigObj.Value<string>(request.ConfigKey) ?? ""
                };
            });
        }

        /// <summary>
        /// 拿到程序服务配置信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ServerAddressReply> GetServerAddress(ServerAddressRequest request, ServerCallContext context)
        {
            return Task.Run<ServerAddressReply>(() =>
            {
                ConfigDcConfig.GetServerAddress(request.ServerName, out string ServerAddress, out string ServerIp, out int ServerPort);
                return new ServerAddressReply()
                {
                    ServerAddress = ServerAddress,
                    ServerIp = ServerIp,
                    ServerPort = ServerPort
                };
            });
        }
    }
}

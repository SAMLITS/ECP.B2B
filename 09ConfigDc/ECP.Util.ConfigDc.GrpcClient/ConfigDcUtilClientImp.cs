using ECP.Util.ConfigDc.Helper;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace ECP.Util.ConfigDc.GrpcClient
{
    public class ConfigDcUtilClientImp
    {  
        private static Channel CreateChannel()
        {
            ConfigDcConfig.GetDcAddress(out string Address, out string Ip, out int Port);
            return new Channel(Address, ChannelCredentials.Insecure);
        }

        /// <summary>
        /// 拿到数据库链接地址
        /// </summary>
        /// <param name="dbKey"></param>
        /// <returns></returns>
        public async static Task<string> GetDbConnectionConfig(string dbKey = null)
        {
            return await Task.Run<string>(() =>
            {
                Channel channel = CreateChannel(); 
                var client = new ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilClient(channel);
                var serverRes = client.GetDbConnectionConfig(new ProtoProxy.DbConfigRequest() { KeyName = dbKey ?? "ConnectionString" });
                channel.ShutdownAsync();   //关闭长连接
                return serverRes.DbConfigVal;

            });
        }

        /// <summary>
        /// 服务发现 根据服务名称进行查找
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public async static Task<string> GetGrpcServiceConfig(string serviceType, string serviceName)
        {
            return await Task.Run<string>(() =>
            {
                Channel channel = CreateChannel();
                
                var client = new ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilClient(channel);
                var serverRes = client.GetGrpcServiceConfig(
                        new ProtoProxy.ServiceFindRequest
                        {
                            ServiceType = serviceType,
                            ServiceName = serviceName
                        }
                    );

                channel.ShutdownAsync();   //关闭长连接
                return serverRes.ServiceAddress;
            });
        }


        /// <summary>
        /// 获取全局配置信息
        /// </summary>
        /// <param name="dbKey"></param>
        /// <returns></returns>
        public async static Task<string> GetApplicationConfig(string ConfigKey )
        {
            return await Task.Run<string>(() =>
            {
                Channel channel = CreateChannel();
                var client = new ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilClient(channel);
                var serverRes = client.GetApplicationConfig(new ProtoProxy.ApplicationConfigRequest() { ConfigKey  = ConfigKey  });
                channel.ShutdownAsync();   //关闭长连接
                return serverRes.ConfigValue;
            });
        }


        /// <summary>
        /// 拿到程序服务配置信息
        /// </summary>
        /// <param name="ConfigKey"></param>
        /// <returns></returns>
        public async static Task<ProtoProxy.ServerAddressReply> GetServerAddress(string ServerName)
        {
            return await Task.Run(() =>
            {
                Channel channel = CreateChannel();
                var client = new ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilClient(channel);
                var serverRes = client.GetServerAddress(new ProtoProxy.ServerAddressRequest() { ServerName = ServerName });
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

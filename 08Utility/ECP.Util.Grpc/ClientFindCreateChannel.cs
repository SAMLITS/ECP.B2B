using ECP.Util.ConfigDc.GrpcClient;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.Util.Grpc
{
    public class ClientFindCreateChannel
    {
        public async static Task<Channel> CreateChannel(string serviceType,string serviceName)
        {
            //服务发现 
            var res = ConfigDcUtilClientImp.GetGrpcServiceConfig(serviceType, serviceName);
            await res;
            if (!string.IsNullOrEmpty(res.Result))
            {
                return new Channel(res.Result, ChannelCredentials.Insecure);
            }
            else
            {
                throw new Exception($"未发现到 <{serviceName} / {serviceType + "_" + serviceName} > 的服务注册配置信息，请检查数据注册中心的 serviceRegiser.json 文件是否未对其进行配置！");
            }
        }
    }
}

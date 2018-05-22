using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.Main.GrpcClient.Interface;
using ECP.B2b.Main.GrpcProxy;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient
{
    public class BaseGrpcExtendCQueryClient<PD, PP, M, QPD, QPP> : BaseGrpcClient<PD, PP, M> , IBaseGrpcExtendCQueryClient<PD, PP, M, QPD, QPP> where PD : class where PP : class, new() where M : class where QPD : class where QPP : class
    {
        protected new Func<Channel, EntityProxyExtendCQueryClient<PD, PP, M, QPD, QPP>> _ProxyClient;
        public BaseGrpcExtendCQueryClient(Func<Channel, EntityProxyExtendCQueryClient<PD, PP, M, QPD, QPP>> _proxyClient):base(_proxyClient)
        { 
             this._ProxyClient = _proxyClient;
        }

        public async Task<PageResult<QPD>> GetCQueryListByPage(QPP queryParams)
        {
            return await Task.Run<PageResult<QPD>>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "CQueryPageService").Result;
                var client = this._ProxyClient(channel);

                var serverRes = client.GetCQueryListByPage(queryParams);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

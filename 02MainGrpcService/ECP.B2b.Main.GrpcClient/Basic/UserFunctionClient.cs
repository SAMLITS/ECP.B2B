using ECP.B2b.ModelDto.Basic.UserFunction;
using Grpc.Core;
using ECP.B2b.Main.GrpcProxy.Basic;
using ECP.B2b.ComEntity.Filter.UserFunction;
using ECP.B2b.DbModel.Basic;
using System;
using ECP.B2b.ComEntity;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECP.Util.Grpc;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 用户功能客户端实现类
    /// </summary>
    public class UserFunctionClient : BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION>, Interface.Basic.IUserFunctionClient
    {
        private Func<Channel, UserFunctionProxy.UserFunctionProxyClient> _ProxyClient;
        public UserFunctionClient() : base((Channel channel) => new UserFunctionProxy.UserFunctionProxyClient(channel)._ReturnThis())
        {
            _ProxyClient = (Channel channel) => new UserFunctionProxy.UserFunctionProxyClient(channel);
        }

        public Task<AjaxResult> SetFunctionByUser(List<B2B_USER_FUNCTION> request)
        {
            return Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "SetFunctionByUser").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.SetFunctionByUser(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

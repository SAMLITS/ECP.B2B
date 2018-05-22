using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity;
using ECP.B2b.ModelDto.System.UserMenu;
using System.Threading.Tasks;
using ECP.Util.Grpc;

namespace ECP.B2b.Main.GrpcClient.System
{
    public class UserMenuClient : BaseGrpcClient<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>, Interface.System.IUserMenuClient
    {
        private Func<Channel, UserMenuProxy.UserMenuProxyClient> _ProxyClient;
        public UserMenuClient() : base((Channel channel) => new UserMenuProxy.UserMenuProxyClient(channel)._ReturnThis())
        {
            _ProxyClient = (Channel channel) => new UserMenuProxy.UserMenuProxyClient(channel);

        }

        public async Task<List<MenuByUserDto>> FindMenuByUser(IdModel request)
        {
            return await Task.Run<List<MenuByUserDto>>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindMenuByUser").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.FindMenuByUser(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public Task<AjaxResult> SetMenuByUser(List<B2B_USER_MENU> request)
        {
            return Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "SetMenuByUser").Result;
                var client = this._ProxyClient(channel);
                var serverRes = client.SetMenuByUser(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

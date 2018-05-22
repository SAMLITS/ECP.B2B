
using ECP.Util.Common;
using ECP.Util.ConfigDc.GrpcClient;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using ECP.Util.Grpc;
using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Sys;
using static ECP.B2b.ComEntity.Filter.Menu.PageQueryParams;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.System;

namespace ECP.B2b.Main.GrpcClient
{
    public class MenuClient: BaseGrpcExtendCQueryClient<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>,Interface.System.IMenuClient
    {
        private Func<Channel, MenuProxy.MenuProxyClient> _MenuProxyClient;
        public MenuClient() : base((Channel channel) => new MenuProxy.MenuProxyClient(channel)._ReturnThis())
        {
            _MenuProxyClient = (Channel channel) => new MenuProxy.MenuProxyClient(channel);
        }

        public Task<List<UserMenuResDto>> GetUserMenus(UserMenuReq request)
        {
            return Task.Run<List<UserMenuResDto>>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "GetUserMenus").Result;
                var client = this._MenuProxyClient(channel);
                var serverRes = client.GetUserMenus(request);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

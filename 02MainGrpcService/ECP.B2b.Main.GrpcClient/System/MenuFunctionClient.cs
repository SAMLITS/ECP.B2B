using ECP.B2b.ModelDto.System.MenuFunction;
using Grpc.Core;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.DbModel.Sys;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 菜单功能客户端实现类
    /// </summary>
    public class MenuFunctionClient : BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION>, Interface.System.IMenuFunctionClient
    {
        public MenuFunctionClient() : base((Channel channel) => new MenuFunctionProxy.MenuFunctionProxyClient(channel)._ReturnThis())
        {
        } 
    }
}

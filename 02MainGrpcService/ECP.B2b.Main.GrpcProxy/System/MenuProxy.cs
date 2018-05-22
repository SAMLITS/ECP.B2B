using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ComEntity.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.Menu;
using ECP.Util.Grpc;
using Grpc.Core;  
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace ECP.B2b.Main.GrpcProxy.System
{

    public static class MenuProxy
    {
        static BaseProtoExtendCQueryProxy<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams> baseProxy = new BaseProtoExtendCQueryProxy<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>(typeof(MenuProxy));
        //单独服务方法在此 Method 定义.....
        public readonly static Method<UserMenuReq, List<UserMenuResDto>> __Method_GetUserMenus = GrpcServiceExtensions.BuildMethod<UserMenuReq, List<UserMenuResDto>>(baseProxy.__ServiceName, "GetUserMenus");


        public static ServerServiceDefinition BindService(IMenuProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....
            builder
               .AddMethod(__Method_GetUserMenus, serviceImpl.GetUserMenus);

            return builder.Build();
        }

        public interface IMenuProxyBase : IEntityProxyBaseExtendCQuery<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>
        {
            //单独服务方法在此扩展  .....

            /// <summary>
            /// 获取用户菜单
            /// </summary>
            /// <param name="request"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            Task<List<UserMenuResDto>> GetUserMenus(UserMenuReq request, ServerCallContext context);
        }


        public class MenuProxyClient : EntityProxyExtendCQueryClient<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>
        {
            public MenuProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                GetUserMenus = (r) => base.MethodTemp(r, __Method_GetUserMenus);
            }

            //单独服务方法在此扩展  .....
            public Func<UserMenuReq, List<UserMenuResDto>> GetUserMenus;
        }
    }
}

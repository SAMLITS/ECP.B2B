using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.ModelDto.System.UserMenu;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy.System
{
    public static class UserMenuProxy
    {
        static BaseProtoBufProxy<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU> baseProxy = new BaseProtoBufProxy<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>(typeof(UserMenuProxy));


        //单独服务方法在此 Method 定义.....
        public readonly static Method<IdModel, List<MenuByUserDto>> __Method_FindMenuByUser = GrpcServiceExtensions.BuildMethod<IdModel, List<MenuByUserDto>>(baseProxy.__ServiceName, "FindMenuByUser");

        public readonly static Method<List<B2B_USER_MENU>, AjaxResult> __Method_SetMenuByUser = GrpcServiceExtensions.BuildMethod<List<B2B_USER_MENU>, AjaxResult>(baseProxy.__ServiceName, "SetMenuByUser");



        public static ServerServiceDefinition BindService(IUserMenuProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....
            builder.AddMethod(__Method_FindMenuByUser, serviceImpl.FindMenuByUser);
            builder.AddMethod(__Method_SetMenuByUser, serviceImpl.SetMenuByUser);

            return builder.Build();
        }

        public interface IUserMenuProxyBase : IEntityProxyBase<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>
        {
            //单独服务方法在此扩展  .....
            Task<List<MenuByUserDto>> FindMenuByUser(IdModel request, ServerCallContext context);

            Task<AjaxResult> SetMenuByUser(List<B2B_USER_MENU> request, ServerCallContext context);
        }


        public class UserMenuProxyClient : EntityProxyClient<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>
        {
            public UserMenuProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                FindMenuByUser = (r) => base.MethodTemp(r, __Method_FindMenuByUser);
                SetMenuByUser = (r) => base.MethodTemp(r, __Method_SetMenuByUser);
            }

            //单独服务方法在此扩展  .....
            public Func<IdModel, List<MenuByUserDto>> FindMenuByUser;
            public Func<List<B2B_USER_MENU>, AjaxResult> SetMenuByUser;
        }

    }
}

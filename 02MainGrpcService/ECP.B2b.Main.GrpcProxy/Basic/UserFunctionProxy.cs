using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.UserFunction;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.ModelDto.Basic.UserFunction;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy.Basic
{

    public static class UserFunctionProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION> baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION>(typeof(UserFunctionProxy));

        //单独服务方法在此 Method 定义.....
        public readonly static Method<List<B2B_USER_FUNCTION>, AjaxResult> __Method_SetFunctionByUser = GrpcServiceExtensions.BuildMethod<List<B2B_USER_FUNCTION>, AjaxResult>(baseProxy.__ServiceName, "SetFunctionByUser");


        public static ServerServiceDefinition BindService(IUserFunctionProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....
            builder.AddMethod(__Method_SetFunctionByUser, serviceImpl.SetFunctionByUser);


            return builder.Build();
        }

        public interface IUserFunctionProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION>
        {
            //单独服务方法在此扩展  .....
            Task<AjaxResult> SetFunctionByUser(List<B2B_USER_FUNCTION> request, ServerCallContext context);

        }


        public class UserFunctionProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION>
        {
            public UserFunctionProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                SetFunctionByUser = (r) => base.MethodTemp(r, __Method_SetFunctionByUser);
            }

            //单独服务方法在此扩展  .....
            public Func<List<B2B_USER_FUNCTION>, AjaxResult> SetFunctionByUser;
        }
    }
}

using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Basic;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.User;
using ECP.Util.Grpc;
using Grpc.Core;  
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy.System
{
    public static class UserProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_USER> baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_USER>(typeof(UserProxy));
        //static UserProxy()
        //{
        //    //当前代码中使用 baseProxy 会为null，因为static构造函数是在类第一次实例化或者任何一个静态属性被调用时才会执行
        //    baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_USER>(typeof(UserProxy));
        //}

        //单独服务方法在此 Method 定义.....
        public readonly static Method<LoginEntity, CurrentUserEntity> __Method_ManagerUserLogin = GrpcServiceExtensions.BuildMethod<LoginEntity, CurrentUserEntity>(baseProxy.__ServiceName, "ManagerUserLogin");

        public readonly static Method<LoginEntity, CurrentUserEntity> __Method_UserLogin = GrpcServiceExtensions.BuildMethod<LoginEntity, CurrentUserEntity>(baseProxy.__ServiceName, "UserLogin");

        public readonly static Method<IdModel, NullableResult> __Method_LastLoginTimeReload = GrpcServiceExtensions.BuildMethod<IdModel, NullableResult>(baseProxy.__ServiceName, "LastLoginTimeReload");

        public readonly static Method<IdModel, NullableResult> __Method_LoginOut = GrpcServiceExtensions.BuildMethod<IdModel, NullableResult>(baseProxy.__ServiceName, "LoginOut");

        public readonly static Method<UserUnBindWxEntity, AjaxResult> __Method_UnBindWx = GrpcServiceExtensions.BuildMethod<UserUnBindWxEntity, AjaxResult>(baseProxy.__ServiceName, "UnBindWx");

        public readonly static Method<UserUpdatePwdEntity, AjaxResult> __Method_UpdatePwd = GrpcServiceExtensions.BuildMethod<UserUpdatePwdEntity, AjaxResult>(baseProxy.__ServiceName, "UpdatePwd");


        public static ServerServiceDefinition BindService(IUserProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....
            builder
                .AddMethod(__Method_ManagerUserLogin, serviceImpl.ManagerUserLogin)
                .AddMethod(__Method_UserLogin, serviceImpl.UserLogin)
                .AddMethod(__Method_LastLoginTimeReload, serviceImpl.LastLoginTimeReload)
                .AddMethod(__Method_LoginOut, serviceImpl.LoginOut)
                .AddMethod(__Method_UnBindWx, serviceImpl.UnBindWx)
                .AddMethod(__Method_UpdatePwd, serviceImpl.UpdatePwd);

            return builder.Build();
        }

        public interface IUserProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_USER>
        {
            //单独服务方法在此扩展  .....

            /// <summary>
            /// 后台管理用户登录
            /// </summary>
            /// <param name="loginEntity"></param>
            /// <returns></returns>
            Task<CurrentUserEntity> ManagerUserLogin(LoginEntity request, ServerCallContext context);

            /// <summary>
            /// 前台用户登录
            /// </summary>
            /// <param name="loginEntity"></param>
            /// <returns></returns>
            Task<CurrentUserEntity> UserLogin(LoginEntity request, ServerCallContext context);

            /// <summary>
            /// 更新指定用户的最后登录时间 与 在线标识
            /// </summary>
            /// <param name="idModel"></param>
            /// <returns></returns>
            Task<NullableResult> LastLoginTimeReload(IdModel request, ServerCallContext context);

            /// <summary>
            /// 退出登录
            /// </summary>
            /// <param name="idModel"></param>
            /// <returns></returns>
            Task<NullableResult> LoginOut(IdModel request, ServerCallContext context);

            /// <summary>
            /// 解除微信绑定
            /// </summary>
            /// <param name="invIoExtend"></param>
            /// <param name="context"></param>
            /// <returns></returns>
            Task<AjaxResult> UnBindWx(UserUnBindWxEntity userUnBindWxEntity, ServerCallContext context);

            Task<AjaxResult> UpdatePwd(UserUpdatePwdEntity userUpdatePwdEntity, ServerCallContext context);
        }

        public class UserProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_USER>
        {
            public UserProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                ManagerUserLogin = (r) => base.MethodTemp(r, __Method_ManagerUserLogin);
                UserLogin = (r) => base.MethodTemp(r, __Method_UserLogin);
                LastLoginTimeReload = (r) => base.MethodTemp(r, __Method_LastLoginTimeReload);
                LoginOut = (r) => base.MethodTemp(r, __Method_LoginOut);
                UnBindWx = (r) => base.MethodTemp(r, __Method_UnBindWx);
                UpdatePwd = (r) => base.MethodTemp(r, __Method_UpdatePwd);
            }

            //单独服务方法在此扩展  .....
            public Func<LoginEntity, CurrentUserEntity> ManagerUserLogin;
            public Func<LoginEntity, CurrentUserEntity> UserLogin;
            public Func<IdModel, NullableResult> LastLoginTimeReload;
            public Func<IdModel, NullableResult> LoginOut; 
            public Func<UserUnBindWxEntity, AjaxResult> UnBindWx;

            public Func<UserUpdatePwdEntity, AjaxResult> UpdatePwd;
        }
    }
}

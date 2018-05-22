using Grpc.Core;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.User;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.ComEntity.CurrentUser;
using System.Threading.Tasks;
using ECP.B2b.ComEntity;
using ECP.B2b.Main.GrpcClient.Interface.System;
using System;
using ECP.Util.Grpc;
using ECP.B2b.ComEntity.Basic;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 用户客户端实现类
    /// </summary>
    public class UserClient: BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_USER>, IUserClient
    {
        private Func<Channel, UserProxy.UserProxyClient> _UserProxyClient;

        public UserClient() : base((Channel channel) => new UserProxy.UserProxyClient(channel)._ReturnThis())
        {
            _UserProxyClient = (Channel channel) => new UserProxy.UserProxyClient(channel);
        }

      

        public Task  LastLoginTimeReload(IdModel idModel)
        {
            return Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "LastLoginTimeReload").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.LastLoginTimeReload(idModel);
                channel.ShutdownAsync();   //关闭长连接
            });
        }

        public Task LoginOut(IdModel idModel)
        {
            return Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "LoginOut").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.LoginOut(idModel);
                channel.ShutdownAsync();   //关闭长连接
            });
        }

        public async Task<CurrentUserEntity> ManagerUserLogin(LoginEntity loginEntity)
        {
            return await Task.Run<CurrentUserEntity>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "ManagerUserLogin").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.ManagerUserLogin(loginEntity);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<CurrentUserEntity> UserLogin(LoginEntity loginEntity)
        {
            return await Task.Run<CurrentUserEntity>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "UserLogin").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.UserLogin(loginEntity);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> UnBindWx(UserUnBindWxEntity userUnBindWxEntity)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "UnBindWx").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.UnBindWx(userUnBindWxEntity);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        public async Task<AjaxResult> UpdatePwd(UserUpdatePwdEntity userUpdatePwdEntity)
        {
            return await Task.Run<AjaxResult>(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "UpdatePwd").Result;
                var client = this._UserProxyClient(channel);
                var serverRes = client.UpdatePwd(userUpdatePwdEntity);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

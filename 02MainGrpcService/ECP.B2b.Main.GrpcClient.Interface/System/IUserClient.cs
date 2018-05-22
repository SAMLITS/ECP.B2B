using ECP.B2b.ModelDto.System.User;
using ECP.B2b.DbModel.Sys;
using System.Threading.Tasks;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Basic;
using ECP.B2b.ComEntity.Filter.User;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    public interface IUserClient : IBaseGrpcClient<PageResultReplyDto, ComEntity.Filter.User.PageQueryParams, B2B_USER>
    {
        /// <summary>
        /// 后台管理用户登录
        /// </summary>
        /// <param name="loginEntity"></param>
        /// <returns></returns>
        Task<CurrentUserEntity> ManagerUserLogin(LoginEntity loginEntity);

        /// <summary>
        /// 前台用户登录
        /// </summary>
        /// <param name="loginEntity"></param>
        /// <returns></returns>
        Task<CurrentUserEntity> UserLogin(LoginEntity loginEntity);

        

        /// <summary>
        /// 更新指定用户的最后登录时间 与 在线标识
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        Task LastLoginTimeReload(IdModel idModel);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        Task LoginOut(IdModel idModel);

        /// <summary>
        /// 解除微信绑定
        /// </summary>
        /// <param name="invIoExtend"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<AjaxResult> UnBindWx(UserUnBindWxEntity userUnBindWxEntity);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="userUpdatePwdEntity"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdatePwd(UserUpdatePwdEntity userUpdatePwdEntity );

    }
}

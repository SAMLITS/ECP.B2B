using ECP.B2b.ComEntity.Basic;
using ECP.B2b.DbModel.Sys;

namespace ECP.B2b.Service.Interface.Basic
{
    public interface IB2B_USER_Service : IBaseService<B2B_USER>
    {
        /// <summary>
        /// 更新指定用户的最后登录时间 与 在线标识
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        void LastLoginTimeReload(int Id);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="idModel"></param>
        /// <returns></returns>
        void LoginOut(int Id);

        /// <summary>
        /// 解绑微信
        /// </summary>
        /// <param name="userWxBind"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UnBindWx(UserUnBindWxEntity userUnBindWxEntity);
    }
}

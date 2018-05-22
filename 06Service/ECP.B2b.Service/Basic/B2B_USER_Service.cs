using ECP.B2b.ComEntity.Basic;
using ECP.B2b.DAL.Interface.Basic;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface.Basic;

namespace ECP.B2b.Service.Basic
{
    public class B2B_USER_Service : BaseService<B2B_USER>, IB2B_USER_Service
    {
        private IB2B_USER_Dal _iuserDal;
        public B2B_USER_Service(IB2B_USER_Dal ibaseDal) : base(ibaseDal)
        {
            this._iuserDal = ibaseDal;
        }

        public void LastLoginTimeReload(int Id)
        {
            this._iuserDal.LastLoginTimeReload(Id);
        }

        public void LoginOut(int Id)
        {
            this._iuserDal.LoginOut(Id);
        }

        /// <summary>
        /// 解绑微信
        /// </summary>
        /// <param name="userWxBind"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UnBindWx(UserUnBindWxEntity userUnBindWxEntity)
        {
            return this._iuserDal.UnBindWx(userUnBindWxEntity);
        }
    }
}

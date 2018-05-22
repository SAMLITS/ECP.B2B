using ECP.B2b.ComEntity.Basic;
using ECP.B2b.DAL.Interface.Basic;
using ECP.B2b.DbModel.Sys;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECP.B2b.DAL.Basic
{
    public class B2B_USER_Dal : BaseDal<B2B_USER>, IB2B_USER_Dal
    {
        public DbContext _userContext { get; set; }
        public B2B_USER_Dal(DbContext hbContext) : base(hbContext)
        {
            _userContext = hbContext;
        }

        public void LastLoginTimeReload(int Id)
        {
            B2B_USER model =  base.Find(Id);
            model.LOGGINED_FLAG = "Y";
            model.LAST_LOGIN_DATE = DateTime.Now;
            base.Update(model);
        }

        public void LoginOut(int Id)
        {
            B2B_USER model = base.Find(Id);
            model.LOGGINED_FLAG = "N";
            base.Update(model);
        }



        /// <summary>
        /// 解绑微信
        /// </summary>
        /// <param name="userWxBind"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UnBindWx(UserUnBindWxEntity userUnBindWxEntity)
        {
            bool isUnBindWxSuccess = true;
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    //更新用户强制微信登录情况
                    if (userUnBindWxEntity.b2b_USER != null)
                    {
                        _userContext.Update(userUnBindWxEntity.b2b_USER);
                        _userContext.SaveChanges();
                    } 
                    _userContext.SaveChanges();
                    tran.Commit();
                    isUnBindWxSuccess = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    isUnBindWxSuccess = false;
                    throw ex;
                }
            }
            return isUnBindWxSuccess;
        }
    }
}

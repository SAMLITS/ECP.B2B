using ECP.B2b.DAL.Interface.Basic;
using ECP.B2b.DbModel.Basic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECP.B2b.DAL.Basic
{
    public class B2B_USER_FUNCTION_Dal : BaseDal<B2B_USER_FUNCTION>, IB2B_USER_FUNCTION_Dal
    {
        public B2B_USER_FUNCTION_Dal(DbContext hbContext) : base(hbContext)
        {
        }

        /// <summary>
        /// 用户菜单分配   删除现有的，插入新的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetFunctionByUser(List<B2B_USER_FUNCTION> request)
        {
            List<B2B_USER_FUNCTION> oldUserFunctions = this.FindAll(m => m.USER_ID == request[0].USER_ID);
            this.TDbSet.RemoveRange(oldUserFunctions);
            this.TDbSet.AddRange(request);
            
            return this.Commit();

        }
    }
}

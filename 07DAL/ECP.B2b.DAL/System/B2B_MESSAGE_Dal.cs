using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DAL.System
{
    public class B2B_MESSAGES_Dal:BaseDal<B2B_MESSAGES>, IB2B_MESSAGES_Dal
    {
        public B2B_MESSAGES_Dal(DbContext dbContext):base(dbContext)
        {

        }

        /// <summary>
        /// 获取当前最大的消息编号
        /// </summary>
        /// <returns></returns>
        public string GetMaxMessagesNumber()
        {
            return this.TDbSet.Max(s=>s.MESSAGE_NUMBER).ToString();
        }
    }
}

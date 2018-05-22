using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.Service.System
{
   public  class B2B_MESSAGES_Service : BaseService<B2B_MESSAGES>, IB2B_MESSAGES_Service
    {
        private IB2B_MESSAGES_Dal _messagesdal;

        public B2B_MESSAGES_Service(IB2B_MESSAGES_Dal messagesdal):base(messagesdal)
        {
            _messagesdal = messagesdal;
        }

        /// <summary>
        /// 获取当前最大的消息编号
        /// </summary>
        /// <returns></returns>
        public string GetMaxMessagesNumber()
        {
            return _messagesdal.GetMaxMessagesNumber();
        }
    }
}

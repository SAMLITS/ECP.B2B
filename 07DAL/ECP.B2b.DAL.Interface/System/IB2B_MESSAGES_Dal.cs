using ECP.B2b.DbModel.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DAL.Interface.System
{
    public interface IB2B_MESSAGES_Dal:IBaseDal<B2B_MESSAGES>
    {
        string GetMaxMessagesNumber();
    }
}

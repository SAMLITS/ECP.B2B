using ECP.B2b.DbModel.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.Service.Interface.System
{
    public interface IB2B_MESSAGES_Service:IBaseService<B2B_MESSAGES>
    {
        string GetMaxMessagesNumber();
    }
}

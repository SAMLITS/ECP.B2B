using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.Service.Interface.System
{
    public interface IB2B_USER_MENU_Service : IBaseService<B2B_USER_MENU>
    {
        bool SetMenuByUser(List<B2B_USER_MENU> request);
    }
}

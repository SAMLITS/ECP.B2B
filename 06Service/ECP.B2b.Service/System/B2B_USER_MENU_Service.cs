using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface.System;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Service.System
{
    public class B2B_USER_MENU_Service : BaseService<B2B_USER_MENU>, IB2B_USER_MENU_Service
    {
        private IB2B_USER_MENU_Dal _iusermenuDal;
        public B2B_USER_MENU_Service(IB2B_USER_MENU_Dal ilookupValuesDal) : base(ilookupValuesDal)
        {
            this._iusermenuDal = ilookupValuesDal;
        }

        public bool SetMenuByUser(List<B2B_USER_MENU> request)
        {
            return _iusermenuDal.SetMenuByUser(request);
        }
    }
}

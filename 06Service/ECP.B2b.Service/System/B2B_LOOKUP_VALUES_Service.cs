using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface.System;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity.Filter.LookUpValues;

namespace ECP.B2b.Service.System
{
    public class B2B_LOOKUP_VALUES_Service : BaseService<B2B_LOOKUP_VALUES>, IB2B_LOOKUP_VALUES_Service
    {
        private IB2B_LOOKUP_VALUES_Dal _ilookupValuesDal;
        public B2B_LOOKUP_VALUES_Service(IB2B_LOOKUP_VALUES_Dal ilookupValuesDal) : base(ilookupValuesDal)
        {
            this._ilookupValuesDal = ilookupValuesDal;
        }
         
    }
}

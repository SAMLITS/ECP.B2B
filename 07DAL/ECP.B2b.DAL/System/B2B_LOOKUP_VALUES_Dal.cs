using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using System.Linq;

namespace ECP.B2b.DAL.System
{
    public class B2B_LOOKUP_VALUES_Dal : BaseDal<B2B_LOOKUP_VALUES>, IB2B_LOOKUP_VALUES_Dal
    {
        public B2B_LOOKUP_VALUES_Dal(DbContext hbContext) : base(hbContext)
        {
        }
         
    }
}

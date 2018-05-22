using ECP.B2b.DbModel.Basic;
using System.Collections.Generic;

namespace ECP.B2b.Service.Interface.Basic
{
    public interface IB2B_USER_FUNCTION_Service : IBaseService<B2B_USER_FUNCTION>
    {
        bool SetFunctionByUser(List<B2B_USER_FUNCTION> request);
    }
}

using ECP.B2b.DAL.Interface.Basic;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.Service.Interface.Basic;
using System.Collections.Generic;

namespace ECP.B2b.Service.Basic
{
    public class B2B_USER_FUNCTION_Service : BaseService<B2B_USER_FUNCTION>, IB2B_USER_FUNCTION_Service
    {
        private IB2B_USER_FUNCTION_Dal _userFunctionDal;
        public B2B_USER_FUNCTION_Service(IB2B_USER_FUNCTION_Dal userFunctionDal) : base(userFunctionDal)
        {
            this._userFunctionDal = userFunctionDal;
        }

        public bool SetFunctionByUser(List<B2B_USER_FUNCTION> request)
        {
            return _userFunctionDal.SetFunctionByUser(request);
        }
    }
}

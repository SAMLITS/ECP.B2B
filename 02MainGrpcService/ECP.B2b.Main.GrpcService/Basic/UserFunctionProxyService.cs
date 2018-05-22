using System;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.Service.Interface;
using ECP.B2b.Main.GrpcProxy.Basic;
using ECP.B2b.ModelDto.Basic.UserFunction;
using ECP.B2b.ComEntity.Filter.UserFunction;
using ECP.B2b.Service.Interface.Basic;
using ECP.B2b.ComEntity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Grpc.Core;

namespace ECP.B2b.Main.GrpcService
{
    public class UserFunctionProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_USER_FUNCTION>, UserFunctionProxy.IUserFunctionProxyBase
    {
        private Func<IB2B_USER_FUNCTION_Service> _userFunctionService;
        public UserFunctionProxyService(Func<IB2B_USER_FUNCTION_Service> useFunctionService) : base(useFunctionService)
        {
            this._userFunctionService = useFunctionService;
        }

        /// <summary>
        /// 用户功能分配  request 至少要有一条数据，即 USER_ID：xx    MENU_ID：0     MENU_FUNCTION_ID=0
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<AjaxResult> SetFunctionByUser(List<B2B_USER_FUNCTION> request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                if (_userFunctionService().SetFunctionByUser(request))
                {
                    return new AjaxResult(DoResult.Success, 3236);
                }
                else
                {
                    return new AjaxResult(DoResult.Failed, 3237);
                }
            });
        }
    }
}

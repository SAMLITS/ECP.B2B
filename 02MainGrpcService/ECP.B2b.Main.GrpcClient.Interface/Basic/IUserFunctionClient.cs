using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.UserFunction;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.ModelDto.Basic.UserFunction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient.Interface.Basic
{
    /// <summary>
    /// 用户功能客户端接口
    /// </summary>
    public interface IUserFunctionClient : IBaseGrpcClient<PageResultReplyDto,PageQueryParams, B2B_USER_FUNCTION>
    {
        Task<AjaxResult> SetFunctionByUser(List<B2B_USER_FUNCTION> request);
    }
}

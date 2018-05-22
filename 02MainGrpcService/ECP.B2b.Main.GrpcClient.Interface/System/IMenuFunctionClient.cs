using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.MenuFunction;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    /// <summary>
    /// 菜单功能客户端接口
    /// </summary>
    public interface IMenuFunctionClient : IBaseGrpcClient<PageResultReplyDto,PageQueryParams, B2B_MENU_FUNCTION>
    { 
    }
}

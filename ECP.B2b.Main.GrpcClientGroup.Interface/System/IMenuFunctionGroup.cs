using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbExtendModel.System;
using ECP.B2b.ModelDto.System.MenuFunction;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.Interface.System
{
    public interface IMenuFunctionGroup
    {
        Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams);
        Task<B2B_MENU_FUNCTION_Extend> FindExtendById(int ID);
    }
}

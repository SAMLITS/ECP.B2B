using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbExtendModel.System;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.Menu;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.Interface.System
{
    public interface IMenuClientGroup
    {
        Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams);
        Task<List<UserMenuResGroupDto>> GetUserMenus(int userId);

        Task<List<BootstrapTreeViewDto>> GetMenusTree(string menuSort);

        /// <summary>
        /// 根据APP用户ID加上主菜单Code查询下面所有的子菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        Task<List<AppUserMenuResDto>> GetAppUserMenus(int userId, string menuCode);

        /// <summary>
        /// 菜单查询控件列表加载
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<PageResult<CQueryPageResultReplyDto>> QueryListByPageGroup(CQueryPageQueryParams queryParams);

        Task<B2B_MENU_Extend> FindExtendById(int ID);
    }
}

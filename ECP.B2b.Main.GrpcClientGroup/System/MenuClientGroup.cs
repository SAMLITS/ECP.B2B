using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.Main.GrpcClient.Interface.System;
using System.Threading.Tasks;
using System.Linq;
using ECP.Util.Common;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ComEntity.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.UserMenu;
using ECP.B2b.ModelDto;
using ECP.Util.Common.Extensions;
using ECP.B2b.DbExtendModel.System;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Main.GrpcClientGroup.System
{
    public class MenuClientGroup : IMenuClientGroup
    {
        public IMenuClient _menuClient;
        public IUserClient _userClient;
        public IUserMenuClient _userMenuClient;
        public ILookUpValuesClient _lookUpValuesClient;
        public ExtendSearchGroup _extendSearchGroup;
        public MenuClientGroup(IMenuClient menuClient, ILookUpValuesClient lookUpValuesClient, IUserClient userClient, IUserMenuClient userMenuClient, ExtendSearchGroup extendSearchGroup)
        {
            _menuClient = menuClient;
            _lookUpValuesClient = lookUpValuesClient;
            _userClient = userClient;
            _userMenuClient = userMenuClient;
            _extendSearchGroup = extendSearchGroup;
        }

        public async Task<List<AppUserMenuResDto>> GetAppUserMenus(int userId, string menuCode)
        {

            var mainMenuTask = _menuClient.FindByEntity(new Dictionary<string, string> {
                    { "MENU_TYPE","M"},
                    { "TERMINAL_TYPE","APP"},
                    { "MENU_CODE",menuCode},
                });
            var userTask = _userClient.FindById(new ComEntity.IdModel { ID = userId });

            await mainMenuTask;
            await userTask;

            //查询主菜单下的所有子菜单
            var subMenuList = _menuClient.FindAllByEntity(new Dictionary<string, string> { { "MAIN_MENU_ID", mainMenuTask.Result.ID.ToString() } }).Result;
            if (userTask.Result.IS_MAIN != "Y")
            {
                //查询指定用户有权限的菜单
                var userMenuTask = _userMenuClient.FindMenuByUser(new ComEntity.IdModel { ID = userId }).Result;
                var userMenuSubMenuIds = userMenuTask.Select(um => um.SUBMENU_ID);
                subMenuList = subMenuList.Where(mu => userMenuSubMenuIds.Contains(mu.ID)).ToList();
            }

            return EntityAutoMapper.ConvertMappingList<AppUserMenuResDto, B2B_MENU>(subMenuList.OrderBy(m => m.ORDER).ToList());
        }

        public Task<List<UserMenuResGroupDto>> GetUserMenus(int userId)
        {
            return Task.Run(() =>
            {
                B2B_USER user = _userClient.FindById(new ComEntity.IdModel { ID = userId }).Result;

                UserMenuReq userMenuReq = new UserMenuReq
                {
                    MENU_SORT = user.PARTY_TYPE,
                    TERMINAL_TYPE = "PC"
                };

                if (user.IS_MAIN != "Y")
                {
                    //不是主账号需要根据菜单分配来过滤
                    List<MenuByUserDto> menuUserList = _userMenuClient.FindMenuByUser(new ComEntity.IdModel { ID = userId }).Result;


                    userMenuReq.IDs = new List<int>();
                    menuUserList.ForEach(mu =>
                    {
                        userMenuReq.IDs.Add(Convert.ToInt32(mu.MAIN_MENU_ID));
                        userMenuReq.IDs.Add(Convert.ToInt32(mu.SUBMENU_ID));
                    });
                }

                List<UserMenuResDto> userMenus = _menuClient.GetUserMenus(userMenuReq).Result;

                //将userMenus查询结果转换为菜单层级对象
                List<UserMenuResGroupDto> userMenuGroups = new List<UserMenuResGroupDto>();
                foreach (var item in userMenus.Where(m => m.MENU_TYPE == "M").OrderBy(m => m.ORDER).ToList())
                {
                    userMenuGroups.Add(new UserMenuResGroupDto
                    {
                        mainUserMenu = item,
                        subUserMenus = userMenus.Where(m => m.MAIN_MENU_ID == item.ID).OrderBy(m => m.ORDER).ToList()
                    });
                };
                return userMenuGroups;
            });
        }

        public Task<List<BootstrapTreeViewDto>> GetMenusTree(string  menuSort)
        {
            return Task.Run(() =>
            {
                List<UserMenuResDto> userMenus = _menuClient.GetUserMenus(new UserMenuReq { MENU_SORT = menuSort, TERMINAL_TYPE = "PC" }).Result;

                //将userMenus查询结果按照路径分配转换为菜单层级对象
                List<BootstrapTreeViewDto> menusTree = new List<BootstrapTreeViewDto>();
                var rootItems = userMenus.Where(m => m.MENU_TYPE == "M" && string.IsNullOrEmpty(m.MENU_PATH)).OrderBy(m => m.ORDER).ToList();
                foreach (var rootItem in rootItems)
                {
                    menusTree.Add(GetMenusTreeData(rootItem, userMenus));
                };
                return menusTree;
            });
        }


        private BootstrapTreeViewDto GetMenusTreeData(UserMenuResDto item, List<UserMenuResDto> menus)
        {
            BootstrapTreeViewDto menusTree = new BootstrapTreeViewDto()
            {
                text=item.MENU_NAME,
                tags=item.ID.ToString(),
                nodes = new List<BootstrapTreeViewDto>()
            };
            var subItems = menus.Where(m => m.MENU_PATH == string.Format("{0}{1},", Convert.ToString(item.MENU_PATH), item.ID)).OrderBy(m => m.ORDER).ToList();
            foreach (var subItem in subItems)
            {
                menusTree.nodes.Add(GetMenusTreeData(subItem, menus));
            }
            return menusTree;
        }

        public async Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams)
        {
            //查出分页数据
            var menuList = await _menuClient.GetListByPage(queryParams);

            if (menuList.Data.Count <= 0) return menuList;
            //获取父层菜单的值
            Task<List<NameByIdDto>> mainMenuDtoList = null;
            mainMenuDtoList = _extendSearchGroup.MenuNameByIdFind(mainMenuDtoList, () => menuList.Data.Select(u => u.MAIN_MENU_ID.ToIntByIntNull()).Distinct().ToList(), "MENU_NAME");


            //获取码表
            _extendSearchGroup.JoinSearchLookup(out var lookupList, "MENU_TYPE", "MENU_SORT", "TERMINAL_TYPE", "YES_NO");

            await mainMenuDtoList;
            await lookupList;


            //列表数据关联值
            menuList.Data.Join(
            lookupList.Result,
            mainMenuDtoList.Result,
            "lookup->MENU_TYPE_NAME->MENU_TYPE->MENU_TYPE",
            "lookup->MENU_SORT_NAME->MENU_SORT->MENU_SORT",
            "lookup->TERMINAL_TYPE_NAME->TERMINAL_TYPE->TERMINAL_TYPE",
            "lookup->IS_AVAILABLE_NAME->YES_NO->IS_AVAILABLE",
            "lookup->IS_ALLOCATED_NAME->YES_NO->IS_ALLOCATED",

            "j1->MAIN_MENU_NAME->MAIN_MENU_ID->ID->NAME1"
            );
            return menuList;
        }

        public async Task<PageResult<CQueryPageResultReplyDto>> QueryListByPageGroup(CQueryPageQueryParams cQueryParams)
        {
            //查出分页数据
            var menuList = await _menuClient.GetCQueryListByPage(cQueryParams);


            if (menuList.Data.Count <= 0) return menuList;
            //获取父层菜单的值
            Task<List<NameByIdDto>> mainMenuDtoList = null;
            mainMenuDtoList = _extendSearchGroup.MenuNameByIdFind(mainMenuDtoList, () => menuList.Data.Select(u => u.MAIN_MENU_ID.ToIntByIntNull()).Distinct().ToList(), "MENU_NAME");


            //获取码表
            _extendSearchGroup.JoinSearchLookup(out var lookupList, "MENU_TYPE", "MENU_SORT", "TERMINAL_TYPE", "YES_NO");

            await mainMenuDtoList;
            await lookupList;


            //列表数据关联值
            menuList.Data.Join(
            lookupList.Result,
            mainMenuDtoList.Result,
            "lookup->MENU_TYPE_NAME->MENU_TYPE->MENU_TYPE",
            "lookup->MENU_SORT_NAME->MENU_SORT->MENU_SORT",
            "lookup->TERMINAL_TYPE_NAME->TERMINAL_TYPE->TERMINAL_TYPE",
            "lookup->IS_AVAILABLE_NAME->YES_NO->IS_AVAILABLE",

            "j1->MAIN_MENU_NAME->MAIN_MENU_ID->ID->NAME1"
            );
            return menuList;
        }
        

        public async Task<B2B_MENU_Extend> FindExtendById(int ID)
        {
            var menu = await _menuClient.FindById(new IdModel { ID = ID});
            var menuExtend = EntityAutoMapper.ConvertMapping<B2B_MENU_Extend, B2B_MENU>(menu);
            //父层菜单
            Task<List<NameByIdDto>> mainMenuDtoList = null;
            mainMenuDtoList = _extendSearchGroup.MenuNameByIdFind(mainMenuDtoList, () => new List<int>() { menu.MAIN_MENU_ID.ToIntByIntNull() }, "MENU_NAME");
            await mainMenuDtoList;
            menuExtend.MAIN_MENU_NAME = mainMenuDtoList.Result.FirstOrDefault()?.NAME1;
        
            return menuExtend;
        }
    }
}

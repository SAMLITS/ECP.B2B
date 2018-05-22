using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbExtendModel.System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.MenuFunction;
using ECP.Util.Common;
using ECP.Util.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.System
{
    public class MenuFunctionGroup : IMenuFunctionGroup
    {
        public IMenuFunctionClient _menuFunctionClient;
        public ExtendSearchGroup _extendSearchGroup;

        public MenuFunctionGroup(IMenuFunctionClient menuFunctionClient, ExtendSearchGroup extendSearchGroup)
        {
            _menuFunctionClient = menuFunctionClient;
            _extendSearchGroup = extendSearchGroup;
        }

        /// <summary>
        /// 组合数据
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams)
        {
            //查出分页数据
            var menuFunctionList = _menuFunctionClient.GetListByPage(queryParams);

            //获取菜单的值
            Task<List<NameByIdDto>> menuDtoList = null;
            menuDtoList = _extendSearchGroup.MenuNameByIdFind(menuDtoList, () => menuFunctionList.Result.Data.Select(u => u.MENU_ID.ToIntByIntNull()).Distinct().ToList(), "MENU_NAME");

            //获取码表
            _extendSearchGroup.JoinSearchLookup(out var lookupList, "YES_NO");

            await menuDtoList;
            await lookupList;

            //列表数据关联值
            menuFunctionList.Result.Data.Join(
            lookupList.Result,
            menuDtoList.Result,
            "lookup->DEFAULT_ASSIGN_NAME->YES_NO->DEFAULT_ASSIGN",
            "j1->MENU_NAME->MENU_ID->ID->NAME1"
            );
            return menuFunctionList.Result;
        }

        /// <summary>
        /// 获取扩展实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<B2B_MENU_FUNCTION_Extend> FindExtendById(int ID)
        {
            var menuFunction = await _menuFunctionClient.FindById(new IdModel { ID = ID });
            var menuFunctionExtend = EntityAutoMapper.ConvertMapping<B2B_MENU_FUNCTION_Extend, B2B_MENU_FUNCTION>(menuFunction);
            //获取菜单的值
            Task<List<NameByIdDto>> menuDtoList = null;
            menuDtoList = _extendSearchGroup.MenuNameByIdFind(menuDtoList, () => new List<int> { menuFunctionExtend.ID}, "MENU_NAME");
            menuFunctionExtend.MENU_NAME = menuDtoList.Result.FirstOrDefault()?.NAME1;
            return menuFunctionExtend;
        }
    }
}

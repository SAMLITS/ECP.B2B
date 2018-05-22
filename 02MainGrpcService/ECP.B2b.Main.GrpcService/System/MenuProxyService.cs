using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface;
using ECP.B2b.Service;
using ECP.B2b.ComEntity.Page;
using System.Linq;
using System.Linq.Expressions;
using ECP.Util.Common;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.System;

namespace ECP.B2b.Main.GrpcService
{
    public class MenuProxyService : ExtendCQueryEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>, MenuProxy.IMenuProxyBase
    {
        public MenuProxyService(Func<IBaseService<B2B_MENU>> _baseService) : base(_baseService)
        {
            //base._ListByPageSelector = m => new B2B_MENU
            //{
            //    ID = m.ID,
            //    MENU_TYPE = m.MENU_TYPE,
            //    MENU_NAME = m.MENU_NAME,
            //    ORDER = m.ORDER,
            //    MENU_URL = m.MENU_URL,
            //    MENU_SORT = m.MENU_SORT,
            //    TERMINAL_TYPE = m.TERMINAL_TYPE,
            //    CREATION_DATE = m.CREATION_DATE,
            //    CREATOR = m.CREATOR,
            //    LAST_UPDATE_DATE = m.LAST_UPDATE_DATE,
            //    EDITOR = m.EDITOR,
            //    MENU_CODE = m.MENU_CODE,
            //    MAIN_MENU_ID=m.MAIN_MENU_ID
            //};
        }

        public Task<List<UserMenuResDto>> GetUserMenus(UserMenuReq request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                List<B2B_MENU> menuList = base.baseService().FindAll(m => m.MENU_SORT == request.MENU_SORT && m.TERMINAL_TYPE == request.TERMINAL_TYPE && (request.IDs == null ? true : request.IDs.Contains(m.ID)) && m.IS_AVAILABLE == "Y").Result;
                if (menuList != null)
                {
                    return EntityAutoMapper.ConvertMappingList<UserMenuResDto, B2B_MENU>(menuList);
                }
                return new List<UserMenuResDto>();
            });
        }

        public Task<List<UserMenuResDto>> GetUserMenuPaths(UserMenuReq request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                List<B2B_MENU> menuList = base.baseService().FindAll(m => m.MENU_SORT == request.MENU_SORT && m.TERMINAL_TYPE == request.TERMINAL_TYPE && (request.IDs == null ? true : request.IDs.Contains(m.ID)) && m.IS_AVAILABLE == "Y").Result;
                if (menuList != null)
                {
                    return EntityAutoMapper.ConvertMappingList<UserMenuResDto, B2B_MENU>(menuList);
                }
                return new List<UserMenuResDto>();
            });
        }

        public override Task<string> ValidDelete(RemoveModel request, ServerCallContext context)
        {
            return Task.Run(() =>
            {

                var findMenu = base.baseService().Find(request.ID, r => new B2B_MENU { MENU_TYPE = r.MENU_TYPE });

                if (findMenu.Result.MENU_TYPE == "M")
                {
                    //如果是主菜单  则校验其下是否存在子菜单，如果存在，那么不可删除
                    var findCount = base.baseService().Count(m => m.MAIN_MENU_ID == request.ID).Result;
                    if (findCount > 0)
                        return "2013";  //还存在子菜单
                    else
                        return "1";
                }
                else
                {
                    return "1";
                }
            });
        }

        public override Task<AjaxResult> Add(B2B_MENU request, ServerCallContext context)
        {
            //对菜单路径MENU_PATH进行处理
            B2B_MENU mainMenuModel = null;
            if (request.MAIN_MENU_ID > 0)
                mainMenuModel = base.FindById(new IdModel { ID = request.MAIN_MENU_ID.Value }, context).Result;
            if (mainMenuModel != null && ((request.MENU_TYPE == "M" && request.MENU_SORT == "ECE") || request.MENU_TYPE == "S"))
            {
                //主菜单类型且为ECE类型时，拼接父层菜单路径MENU_PATH
                request.MENU_PATH = string.Format("{0},", mainMenuModel.ID.ToString());
                if (!string.IsNullOrEmpty(mainMenuModel.MENU_PATH))
                    request.MENU_PATH = string.Format("{0}{1}", mainMenuModel.MENU_PATH, request.MENU_PATH);
            }
            return base.Add(request, context);
        }
    }
}

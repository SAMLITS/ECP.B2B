using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto;
using ECP.B2b.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.ComEntity;
using ECP.B2b.ModelDto.System.Menu;
using Grpc.Core;
using System.Threading.Tasks;
using ECP.B2b.ModelDto.System.UserMenu;
using ECP.Util.Common;
using ECP.B2b.Service.Interface.System;

namespace ECP.B2b.Main.GrpcService.System
{
    public class UserMenuProxyService : EntityProxyBase<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>, UserMenuProxy.IUserMenuProxyBase
    {
        private Func<IB2B_USER_MENU_Service> _userMenuService;
        public UserMenuProxyService(Func<IB2B_USER_MENU_Service> userMenuService) : base(userMenuService)
        {
            this._userMenuService = userMenuService;
        }

        public Task<List<MenuByUserDto>> FindMenuByUser(IdModel request, ServerCallContext context)
        {
            return Task.Run(()=> 
            {
                List< B2B_USER_MENU > userMenuList =  base.baseService().FindAll(m => m.USER_ID == request.ID, m => new B2B_USER_MENU
                {
                    ID = m.ID,
                    MAIN_MENU_ID = m.MAIN_MENU_ID,
                    SUBMENU_ID = m.SUBMENU_ID
                }).Result;

                if (userMenuList != null)
                {
                    return EntityAutoMapper.ConvertMappingList<MenuByUserDto, B2B_USER_MENU>(userMenuList);
                }
                return new List<MenuByUserDto>();
            });

        }

        /// <summary>
        /// 用户菜单分配  request 至少要有一条数据，即 USER_ID：xx    MAIN_MENU_ID：0     SUBMENU_ID=0
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<AjaxResult> SetMenuByUser(List<B2B_USER_MENU> request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                if (_userMenuService().SetMenuByUser(request))
                {
                    return new AjaxResult(DoResult.Success, 2010);
                }
                else
                {
                    return new AjaxResult(DoResult.Failed, 2011);
                }
            });
        }
    }
}
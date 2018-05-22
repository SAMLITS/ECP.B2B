using System;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Service.Interface;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.MenuFunction;
using ECP.B2b.ComEntity.Filter.MenuFunction;

namespace ECP.B2b.Main.GrpcService
{
    public class MenuFunctionProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_MENU_FUNCTION>, MenuFunctionProxy.IMenuFunctionProxyBase
    {
        public MenuFunctionProxyService(Func<IBaseService<B2B_MENU_FUNCTION>> _baseService) : base(_baseService)
        {
        }
    }
}

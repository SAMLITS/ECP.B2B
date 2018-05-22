using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECP.Util.HtmlHelper;

namespace ECP.B2b.Manager.ViewComponents
{
    [ViewComponent]
    public class MenuNavigationViewComponent : ViewComponent
    {
        public IMenuClientGroup menuClientGroup;
        public MenuNavigationViewComponent(IMenuClientGroup _menuClientGroup)
        {
            menuClientGroup = _menuClientGroup;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var resultReply = await menuClientGroup.GetUserMenus(HttpContext.GetCurrentUser().ID);
            ViewBag.MenuList = resultReply;
            return View(resultReply);
        }

    }
}

using ECP.B2b.Manager.Models;
using ECP.B2b.ModelDto.System.MenuFunction;
using ECP.B2b.ComEntity.Filter.MenuFunction;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.DbModel.Sys;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using ECP.B2b.ModelDto;
using System.Collections.Generic;
using System.Linq;
using ECP.B2b.Main.GrpcClientGroup;
using System;

namespace ECP.B2b.Manager.Areas.System.Controllers
{
    public class MenuFunctionController: BaseController<PageResultReplyDto,PageQueryParams, B2B_MENU_FUNCTION>
    { 
        public IMenuFunctionClient _menuFunctionClient;
        public IMenuFunctionGroup _menuFunctionGroup;
        public ExtendSearchGroup _extendSearchGroup;
        public MenuFunctionController(IMenuFunctionClient menuFunctionClient, IMenuFunctionGroup menuFunctionGroup, ExtendSearchGroup extendSearchGroup) :base(menuFunctionClient)
        {
            _menuFunctionClient = menuFunctionClient;
            _menuFunctionGroup = menuFunctionGroup;
            _extendSearchGroup = extendSearchGroup;
        }

        [HttpGet]
        public override ViewResult List()
        {
            ViewBag.MENU_ID = Request.Query["MENU_ID"];
            return View();
        }

        [HttpGet]
        public override ActionResult Add()
        {
            var menuIdStr=Request.Query["MENU_ID"];
            ViewBag.MENU_ID = menuIdStr;
            //获取菜单名称的值
            Task<List<NameByIdDto>> menuDtoList = null;
            menuDtoList = _extendSearchGroup.MenuNameByIdFind(menuDtoList, () => new List<int> { Convert.ToInt32(menuIdStr) }, "MENU_NAME");
            ViewBag.MENU_NAME = menuDtoList.Result.FirstOrDefault()?.NAME1;
            return View();
        }

        [HttpPost]
        public override async Task<JsonResult> List(PageQueryParams pageQueryParams)
        {
            var resultReply = await _menuFunctionGroup.ListByPageGroup(pageQueryParams);
            return Json(resultReply);
        }


        /// <summary>
        /// 获取扩展类（包含菜单名称）
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindExtendById(int ID)
        {
            var resultReply = _menuFunctionGroup.FindExtendById(ID);
            return Json(resultReply.Result);
        }
    }
}

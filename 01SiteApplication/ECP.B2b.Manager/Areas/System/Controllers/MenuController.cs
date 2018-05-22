using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.ComEntity;

using ECP.B2b.ComEntity.Page;
using ECP.B2b.Manager.Models;
using ECP.Util.Common;
using ECP.B2b.Main.GrpcClient.Interface.System;
using Microsoft.AspNetCore.Http;
using static ECP.B2b.ComEntity.Filter.Menu.PageQueryParams;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;

namespace ECP.B2b.Manager.Areas.System.Controllers
{
    public class MenuController : BaseController<ModelDto.System.Menu.PageResultReplyDto, ComEntity.Filter.Menu.PageQueryParams, B2B_MENU>
    {
        public IMenuClient _menuClient;
        public IMenuClientGroup _menuClientGroup;
        public MenuController(IMenuClient menuClient, IMenuClientGroup menuClientGroup) : base(menuClient)
        {
            _menuClient = menuClient;
            _menuClientGroup = menuClientGroup;
        }

        public override ActionResult Add()
        {
            ViewBag.MENU_TYPE = Request.Query["MENU_TYPE"];
            ViewBag.MAIN_MENU_ID = Request.Query["MAIN_MENU_ID"];
            ViewBag.MAIN_MENU_NAME = Request.Query["MAIN_MENU_NAME"];
            ViewBag.MENU_SORT = Request.Query["MENU_SORT"];
            ViewBag.TERMINAL_TYPE = Request.Query["TERMINAL_TYPE"];
            return View();
        }

        public override ActionResult Modify()
        {
            return View();
        }

        public override async Task<JsonResult> List(PageQueryParams pageQueryParams)
        {
            var resultReply = await _menuClientGroup.ListByPageGroup(pageQueryParams);
            return Json(resultReply);
        }

        /// <summary>
        /// 获取扩展类（包含父层菜单名称等）
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindExtendById(int ID)
        {
            var resultReply = _menuClientGroup.FindExtendById(ID);
            return Json(resultReply.Result);
        }

        //菜单查询控件
        public async Task<JsonResult> CQueryList(CQueryPageQueryParams cQueryPageQueryParams)
        {
            var resultReply = await _menuClientGroup.QueryListByPageGroup(cQueryPageQueryParams);
            return Json(resultReply);
        }

        /// <summary>
        /// 查看层次结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowPaths(string MENU_SORT)
        {
            ViewBag.MENU_SORT = MENU_SORT;
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetMenusTree(string MENU_SORT)
        {
            var resultReply = await _menuClientGroup.GetMenusTree(MENU_SORT);
            return Json(resultReply);
        }
    }
}
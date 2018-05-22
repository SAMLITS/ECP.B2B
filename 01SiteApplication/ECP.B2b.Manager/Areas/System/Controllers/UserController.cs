using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Manager.Models;
using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.Util.HtmlHelper.Filter;
using ECP.Util.Common.Extensions;
using ECP.B2b.DbModel.Basic;
using ECP.Util.HtmlHelper.Filter.Attributes;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECP.B2b.Manager.Areas.System.Controllers
{ 
    //[FunctionFilterAttribute("Modify_modify_user,Authority_xxxxx")]
    public class UserController : BaseController<ModelDto.System.User.PageResultReplyDto, ComEntity.Filter.User.PageQueryParams, B2B_USER>
    {
        public IUserClient userClient;
        public IUserMenuClient userMenuClient;
        public IMenuClient menuClient;
        public IUserGroup userGroup; 
        public IMenuFunctionClient _menuFunctionClient;
        public IUserFunctionClient _userFunctionClient;

        public UserController(IUserClient _userClient, IUserMenuClient _userMenuClient,IMenuClient _menuClient, IUserGroup _userGroup,   IMenuFunctionClient menuFunctionClient, IUserFunctionClient userFunctionClient) :base(_userClient)
        {
            userClient = _userClient;
            this.userMenuClient = _userMenuClient;
            this.menuClient = _menuClient;
            this.userGroup = _userGroup; 
            _menuFunctionClient = menuFunctionClient;
            _userFunctionClient = userFunctionClient;
        }

        /// <summary>
        /// 重写新增用户视图
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public override ActionResult Add()
        {
            //“密码”默认值取自系统参数设置“DEFAULT_PASSWORD”的值
            ViewBag.UserPassword = "DDD";
            return base.Add();
        }

        /// <summary>
        /// 重写用户新增保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override Task<JsonResult> Add(B2B_USER request)
        {
            request.PARTY_TYPE = CurrentUser.PARTY_TYPE;
            request.PARTY_ID = CurrentUser.PARTY_ID;
            //“注册状态”自动赋值为“1-已生效”
            request.REG_STATUS = "1";
            return base.Add(request);
        }

        /// <summary>
        /// 重写编辑用户视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Modify()
        {
            ViewBag.ID = Request.Query["ID"];
            return base.Modify();
        }

        /// <summary>
        /// 获取扩展类（交易方名称）
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]

        public  JsonResult FindExtendById(int ID)
        {
            var resultReply =userGroup.FindExtendById(ID);
            return Json(resultReply.Result);
        }

        /// <summary>
        /// 重写列表数据加载，处理码值
        /// </summary>
        /// <param name="pageQueryParams"></param>
        /// <returns></returns>
        public override async Task<JsonResult> List(PageQueryParams pageQueryParams)
        {
            if (CurrentUser.PARTY_TYPE == "FIN")
            {
                //金融方默认只加载当前用户所属交易方
                pageQueryParams.QueryParams.PARTY_ID = new List<int> {CurrentUser.PARTY_ID };
            }
            var resultReply = await userGroup.ListByPageGroup(pageQueryParams);
            return Json(resultReply);
        }

        /// <summary>
        /// 菜单分配页面
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet] 
        public async Task<ViewResult> Authority(int ID)
        {
        var user = this.userClient.FindById(new IdModel { ID=ID });
            //返回菜单数据
            var menus = this.menuClient.FindAll();
            //返回已有权限
            var userMenus = userMenuClient.FindMenuByUser(new IdModel { ID = ID });

            await user;
            await menus;
            await userMenus;

            ViewBag.Menus = menus.Result.Where(m => m.MENU_SORT == user.Result.PARTY_TYPE && m.TERMINAL_TYPE == "PC"&&m.IS_AVAILABLE=="Y"&& m.IS_ALLOCATED=="Y").ToList();
            ViewBag.UserMenus = userMenus.Result;
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        public JsonResult Authority(List<B2B_USER_MENU> userMenus)
        {
            if (userMenus == null || userMenus.Count == 0)
            {
                userMenus = new List<B2B_USER_MENU> { new B2B_USER_MENU { USER_ID = CurrentUser.ID, MAIN_MENU_ID = 0, SUBMENU_ID = 0 } };
            }

            AjaxResult res = userMenuClient.SetMenuByUser(userMenus).Result;
            return Json(res);
        }

        /// <summary>
        /// 功能分配页面
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Function(int ID)
        {
            var user = this.userClient.FindById(new IdModel { ID = ID });
            //返回菜单数据
            var menus = this.menuClient.FindAll();
            //返回功能数据
            var menuFunctions = _menuFunctionClient.FindAll();
            //返回已有权限
            var userMenus = userMenuClient.FindMenuByUser(new IdModel { ID = ID });
            //返回已有功能权限
            var userFunctions = _userFunctionClient.FindAllByEntity(new Dictionary<string, string> { { "USER_ID", ID.ToString() } });
            await user;
            await menus;
            await userMenus;
            await userFunctions;

            //仅显示已分配权限的菜单
            var VisibleMenus = menus.Result.Where(m => userMenus.Result.Select(u => u.MAIN_MENU_ID).Contains(m.ID) || userMenus.Result.Select(u => u.SUBMENU_ID).Contains(m.ID)).ToList();
            ViewBag.Menus = VisibleMenus;
            //获取对应菜单下功能
            ViewBag.MenuFunctions= menuFunctions.Result.Where(mf => VisibleMenus.Select(m => m.ID).Contains(mf.MENU_ID.ToIntByIntNull())).ToList();
            ViewBag.UserFunctions = userFunctions.Result;
            ViewBag.ID = ID;
            return View();
        }

        [HttpPost]
        public JsonResult Function(List<B2B_USER_FUNCTION> userFunctions)
        {
            if (userFunctions == null || userFunctions.Count == 0)
            {
                userFunctions = new List<B2B_USER_FUNCTION> { new B2B_USER_FUNCTION { USER_ID = CurrentUser.ID, MENU_ID = 0, MENU_FUNCTION_ID = 0 } };
            }

            AjaxResult res = _userFunctionClient.SetFunctionByUser(userFunctions).Result;
            return Json(res);
        }

        [HttpPost]
        public override JsonResult FindById(int ID)
        {
            B2B_USER dbModel = userClient.FindById(new IdModel { ID = ID }).Result;
            return Json(dbModel);
        }

        /// <summary>
        /// 个人设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult PersonalSet()
        {
            return View();
        }

        /// <summary>
        /// /解除绑定
        /// </summary>
        /// <param name="unBindWxDic"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UnBindWx(Dictionary<string, string> unBindWxDic)
        {
            var reply = await userGroup.UnBindWx(unBindWxDic);
            return Json(reply.Result);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="pwdDic"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangePwd(Dictionary<string, string> pwdDic)
        {
            var reply = await userGroup.ChangePwd(pwdDic);
            return Json(reply);
        }

        /// <summary>
        /// 跳转到ECP后台
        /// </summary>
        [HttpGet]
        public void OpenRedirectEcp()
        {
            var ecpaddress = ECP_B2B_API_SDK.Helper.EcpB2bOpenChangeHelper.GetOpenEcpB2bAddressUrl(base.CurrentUser.ID);
            Response.Redirect(ecpaddress);
        }

    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using ECP.B2b.ComEntity.CurrentUser;
using Microsoft.AspNetCore.Mvc;
using ECP.B2b.ComEntity;
using Microsoft.AspNetCore.Routing;
using ECP.Util.HtmlHelper.Filter.Attributes;
using System.Linq;

namespace ECP.Util.HtmlHelper.Filter
{
    /// <summary>
    /// 检查用户登录
    /// </summary>
    /// <param name="context"></param>
    public class LoginClientAuthorityFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUser = context.HttpContext.GetCurrentUser();

                if (currentUser.IS_MAIN != "Y" && currentUser.PARTY_TYPE != "SYS")
                {
                    //已登录   校验是否有相应Action权限
                    var actionFunc = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
                    var functionAttrs = actionFunc.MethodInfo.GetCustomAttributes(typeof(FunctionFilterAttribute), true);

                    var funCode = "";
                    if (functionAttrs.Length > 0)
                    {
                        var functionAttr = (FunctionFilterAttribute)functionAttrs[0];
                        funCode = functionAttr.FunCode;
                    }

                    if (string.IsNullOrEmpty(funCode))
                    {
                        //查询 Controlls 上是否标记父类中的 Action 权限
                        functionAttrs = actionFunc.ControllerTypeInfo.GetCustomAttributes(typeof(FunctionFilterAttribute), true);
                        if (functionAttrs.Length > 0)
                        {
                            var actionName = actionFunc.ActionName;
                            var functionAttr = (FunctionFilterAttribute)functionAttrs[0];

                            var funcList = functionAttr.FunCode.Split(",");
                            if (funcList.Count(c => c.StartsWith(actionName + "_")) > 0)
                            {
                                funCode = funcList.First(c => c.StartsWith(actionName + "_"));
                                funCode = funCode.Substring((actionName + "_").Length);
                            }
                        }
                    }

                    //funCode 为 空 --> 则代表不需要权限过滤，完全开放
                    //funCode 不为 空 --> 则代表需要权限过滤，执行下列代码
                    if (!string.IsNullOrEmpty(funCode))
                    {
                        var funcList = currentUser.UserFunctionEnabledList;
                        if (!funcList.Contains(funCode))
                        {
                            //权限列表中不存在   无权限
                            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                                context.Result = new JsonResult(new AjaxResult("无权限访问！", DoResult.NotAuthority));
                            else
                                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "User", action = "NotAuthority" }));
                        }
                    }
                }

                return;
            }

            //context.HttpContext.Session.SetString("CurrentUrl", context.HttpContext.Request.Path);

            //判断是否是ajax请求
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                context.Result = new JsonResult(new AjaxResult("身份验证过期，请重新登录！", DoResult.OverTime));
            else
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "User", action = "Login" }));
        }
    }
}
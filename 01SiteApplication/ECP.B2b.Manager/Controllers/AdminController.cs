using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ECP.Util.Common;
using Microsoft.AspNetCore.Http;
using ECP.Util.HtmlHelper; 
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.Util.HtmlHelper.Filter;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.DbModel.Basic;
using ECP.B2b.ComEntity; 
using ECP.B2b.Main.GrpcClient;
using Microsoft.AspNetCore.Authorization;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.Util.Jwt.LTS;
using static ECP.Util.HtmlHelper.UserManage; 


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECP.B2b.Manager.Controllers
{
    public class AdminController : Controller
    {
        public Logger logger = new Logger(typeof(AdminController));
        public IUserClient userClient; 
        public AdminController(IUserClient _userClient )
        {
            userClient = _userClient; 
        }
        //[Authorize("Bearer")]
        [LoginManagerAuthorityFilter]
        public IActionResult Index()
        {
            return View(HttpContext.GetCurrentUser());
        }
        [HttpGet]
        public ActionResult NotAuthority()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string flag = "")
        {
            ViewBag.Error = flag;
            return View();
        }
        public ActionResult Main()
        {
          
            return View();
        }

        [HttpGet]
        public IActionResult PrimaryLogin()
        {
            var secretStr = Request.QueryString.ToString();
            var secretDesEncryptHelper = ECP_B2B_API_SDK.Helper.SecretDesEncryptHelper.secretDefaultEncryptHelper;
            var loginEnityJson = secretDesEncryptHelper.GetDecryptDicObject(secretStr);
            var userId = Convert.ToInt32(loginEnityJson["userId"]);


            LoginResultEntity loginResultEntity = null;
            try
            {
                //调用统一登录授权平台
                HttpClientHelper<LoginUser>.SendGetSync(string.Format(ApplicationConfig.ManagerPrimaryLoginTokenAddress, userId), s =>
                {
                    loginResultEntity = JsonHelper.ToObject<LoginResultEntity>(s.ReadToEnd());
                });
            }
            catch (Exception ex)
            {
                logger.Error("登录授权错误！", ex);
                //授权失败！
                return this.RedirectToAction("Login", new { flag = "3106" });
            }

            UserManage.LoginResult result = loginResultEntity.loginResult;
            if (result == UserManage.LoginResult.WaitAudit)
                return this.RedirectToAction("Login", new { flag = "2003" });
            else if (result == UserManage.LoginResult.Closed)
                return this.RedirectToAction("Login", new { flag = "2004" });
            else if (result == UserManage.LoginResult.PartyStatusEx)
                return this.RedirectToAction("Login", new { flag = "2005" });

            if (result == UserManage.LoginResult.Success)
            {
                
                    //token 设置
                    HttpContext.Response.Cookies.Append("userToken", loginResultEntity.toKen);
                    string CurrentUrl = this.HttpContext.Session.GetString("CurrentUrl");
                    if (CurrentUrl == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.HttpContext.Session.Remove("CurrentUrl");
                        return Redirect(CurrentUrl);
                    }  
            }
            else
            {
                return this.RedirectToAction("Login", new { flag = "2006" });
            }
        }


        [HttpPost]
        public IActionResult Login(string USER, string PASSWORD, string CheckCode)
        {
            //验证码 
            var LoginValidateCode = HttpContext.Session.GetString("LoginValidateCode");
            HttpContext.Session.Remove("LoginValidateCode");
            if (string.IsNullOrEmpty(CheckCode) 
                || LoginValidateCode == null 
                || !LoginValidateCode.Equals(CheckCode, StringComparison.OrdinalIgnoreCase))
            {
                return this.RedirectToAction("Login", new { flag = "2002" });
            } 
           
            LoginResultEntity loginResultEntity = null;
            try
            {
                //调用统一登录授权平台
                HttpClientHelper<LoginUser>.SendPostSync(new LoginUser { Username = USER, Password = PASSWORD }, ApplicationConfig.ManagerLoginTokenAddress, s =>
                {
                    loginResultEntity = JsonHelper.ToObject<LoginResultEntity>(s.ReadToEnd());
                });
            }
            catch (Exception ex)
            {
                logger.Error("登录授权错误！", ex);
                //授权失败！
                return this.RedirectToAction("Login", new { flag = "3106" });
            }

            UserManage.LoginResult result = loginResultEntity.loginResult;  
              if (result == UserManage.LoginResult.WaitAudit)
                return this.RedirectToAction("Login", new { flag = "2003" });
            else if (result == UserManage.LoginResult.Closed)
                return this.RedirectToAction("Login", new { flag = "2004" });
            else if (result == UserManage.LoginResult.PartyStatusEx)
                return this.RedirectToAction("Login", new { flag = "2005" });

            if (result == UserManage.LoginResult.Success)
            {
                
                    //token 设置
                    HttpContext.Response.Cookies.Append("userToken", loginResultEntity.toKen);
                    string CurrentUrl = this.HttpContext.Session.GetString("CurrentUrl");
                    if (CurrentUrl == null)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.HttpContext.Session.Remove("CurrentUrl");
                        return Redirect(CurrentUrl);
                    }
                
            }
            else
            {
                return this.RedirectToAction("Login", new { flag = "2006" });
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            this.HttpContext.UserLogout(userClient);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            (byte[] bytes,string type)  = this.HttpContext.ValidateCodeBuild();
            return File(bytes, type);
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
         
    }
}

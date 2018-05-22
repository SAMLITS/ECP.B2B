using ECP.B2b.ComEntity.CurrentUser;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.Util.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECP.Util.HtmlHelper;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.DbModel.Basic; 
using ECP.B2b.DbModel.Sys;  


namespace ECP.Util.HtmlHelper
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public static class UserManage
    {
        private static Logger logger = new Logger(typeof(UserManage));

        /// <summary>
        /// 登录来源   前台登录/后台登录
        /// </summary>
        public enum LoginSource
        {
            Client,
            Manager
        }

        /// <summary>
        /// 0成功 1用户不存在 2密码错误 3 验证码错误 4账号已冻结
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        public static LoginResultEntity UserLogin(this HttpContext context, IUserClient userClient,  IUserFunctionClient userFunctionClient, Func<CurrentUserEntity,string> tokenBuildFunc, string name = "", string pwd = "", string verify = "", LoginSource loginSource = LoginSource.Client,int userId=0)
        {

            //验证码 
            //var LoginValidateCode = context.Session.GetString("LoginValidateCode");
            //context.Session.Remove("LoginValidateCode");
            //if (string.IsNullOrEmpty(verify) || LoginValidateCode == null || !LoginValidateCode.Equals(verify, StringComparison.OrdinalIgnoreCase))
            //{
            //    return LoginResult.WrongVerify;
            //}

            CurrentUserEntity currentUser;
            if (userId != 0)
            {
                var findUser = userClient.FindById(new B2b.ComEntity.IdModel { ID = userId }).Result;
                currentUser = new CurrentUserEntity()
                {
                    END_DATE = findUser.END_DATE,
                    ID = findUser.ID,
                    IS_MAIN = findUser.IS_MAIN,
                    IS_WXIN_LOGIN = findUser.IS_WXIN_LOGIN,
                    MAIL = findUser.MAIL,
                    MOBILE = findUser.MOBILE,
                    PARTY_ID = Convert.ToInt32(findUser.PARTY_ID),
                    PARTY_TYPE = findUser.PARTY_TYPE,
                    REG_STATUS = findUser.REG_STATUS,
                    START_DATE = findUser.START_DATE,
                    USER = findUser.USER,
                    USER_NAME = findUser.USER_NAME
                };
            }
            else
            {
                if (loginSource == LoginSource.Client)
                    currentUser = userClient.UserLogin(new LoginEntity { USER = name, PASSWORD = pwd }).Result;
                else
                    currentUser = userClient.ManagerUserLogin(new LoginEntity { USER = name, PASSWORD = pwd }).Result;
            }
            if (currentUser == null || currentUser.ID == 0)
            {
                return new LoginResultEntity(LoginResult.WrongLogin);
            }
            else
            {
                if (currentUser.REG_STATUS == "0")
                    return new LoginResultEntity(LoginResult.WaitAudit);
                //用户有效性判断 （且"当前日期"在“生效日期START_DATE”与“失效日期END_DATE”之间）
                else if (currentUser.REG_STATUS == "2" || currentUser.START_DATE == null || DateTime.Now < currentUser.START_DATE || (currentUser.END_DATE != null && DateTime.Now >= currentUser.END_DATE))

                    return new LoginResultEntity(LoginResult.Closed); 

                var userFunctions = userFunctionClient.FindAllByEntity(new Dictionary<string, string> { { "USER_ID", currentUser.ID.ToString() } }).Result;
                for (int i = 0; i < userFunctions.Count; i++)
                {
                    currentUser.UserFunctionEnabledList.Add(userFunctions[i].MENU_FUNCTION_CODE);
                }


                string token = tokenBuildFunc(currentUser);
                //context.Session.SetString("CurrentUser", JsonHelper.ToJson<CurrentUserEntity>(currentUser));
                logger.Debug(string.Format("用户id={0} Name={1}登录系统", currentUser.ID, currentUser.USER));

                userClient.LastLoginTimeReload(new ECP.B2b.ComEntity.IdModel { ID = currentUser.ID });

                return new LoginResultEntity(LoginResult.Success, token) {  userId = currentUser.ID };
            }
        }


        public enum LoginResult
        {
            /// <summary>
            /// 账号待处理
            /// </summary>
            WaitAudit = 0,
            /// <summary>
            /// 登录成功
            /// </summary>
            Success = 1,

            /// <summary>
            /// 账号已失效
            /// </summary>
            Closed = 2,



            /// <summary>
            /// 用户名或密码错误 
            /// </summary>
            WrongLogin = 3,
            /// <summary>
            /// 验证码错误
            /// </summary>
            WrongVerify = 4,

            /// <summary>
            /// 交易方状态异常
            /// </summary>
            PartyStatusEx = 5
        }
        public class LoginResultEntity
        {
            public LoginResultEntity(LoginResult _loginResult, string _toKen="")
            {
                loginResult = _loginResult;
                toKen = _toKen;
            }

            public LoginResult loginResult { get; set; }
            public string toKen { get; set; }
            public int userId { get; set; } 

        }

        /// <summary>
        /// App登录返回信息
        /// </summary>
        public class AppLoginResultEntity
        {
            public AppLoginResultEntity()
            {
                this.appCurrentUserEntity = new AppCurrentUserEntity();
            }

            public LoginResultEntity loginResultEntity { get; set; }
            public AppCurrentUserEntity appCurrentUserEntity { get; set; }
        }


        /// <summary>
        /// 返回当前登录用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static CurrentUserEntity GetCurrentUser(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string userInfo = ((System.Security.Claims.ClaimsIdentity)context.User.Identity).Claims.Where(c => c.Type == "userInfo").First().Value;
                return JsonHelper.ToObject<CurrentUserEntity>(userInfo);
            }
            return default(CurrentUserEntity);
        }
 
       
        /// <summary>
        /// 0成功 1用户不存在 2密码错误 3 验证码错误 4账号已冻结
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void UserLogout(this HttpContext context, IUserClient userClient)
        {
            #region 清理 Token
            CurrentUserEntity currentUser = context.GetCurrentUser();
            if (currentUser != default(CurrentUserEntity))
                logger.Debug(string.Format("用户id={0} Name={1} 退出系统", currentUser.ID, currentUser.USER));

            if (currentUser != null)
                userClient.LoginOut(new ECP.B2b.ComEntity.IdModel { ID = currentUser.ID });

            context.Response.Cookies.Delete("userToken");
            context.Session.Clear();
            #endregion Session
        }
    }
}
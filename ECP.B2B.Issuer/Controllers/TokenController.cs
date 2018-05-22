using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECP.Util.Jwt.LTS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using ECP.Util.HtmlHelper;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.Util.Common;
using ECP.Util.Common.DEncrypt;
using static ECP.Util.HtmlHelper.UserManage;

namespace ECP.B2B.Issuer.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {

        public IUserClient userClient; 
        public IUserFunctionClient userFunctionClient;

        private readonly JWTTokenOptions _tokenOptions;

        public TokenController(
            JWTTokenOptions tokenOptions,
            IUserClient _userClient, 
            IUserFunctionClient _userFunctionClient)
        {
            _tokenOptions = tokenOptions;
            userClient = _userClient; 
            userFunctionClient = _userFunctionClient;
        }
         

        /// <summary>
        /// 生成一个新的 Token
        /// </summary>
        /// <param name="user">用户信息实体</param>
        /// <param name="expire">token 过期时间</param>
        /// <param name="audience">Token 接收者</param>
        /// <returns></returns>
        private string CreateToken(dynamic user, DateTime expire, string audience)
        {
            var handler = new JwtSecurityTokenHandler();
            string jti = audience + user.USER + new TimeSpan(expire.Ticks).TotalMilliseconds;
            jti = jti.GetMd5(); // Jwt 的一个参数，用来标识 Token
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, string.Empty), // 添加角色信息
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), // 用户 Id ClaimValueTypes.Integer32),
                new Claim("jti",jti,ClaimValueTypes.String), // jti，用来标识 token
                new Claim("userInfo",JsonHelper.ToJson(user))
            };
            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.USER, "TokenAuth"), claims);
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {
                Issuer = ApplicationConfig.Issuer, // 指定 Token 签发者，也就是这个签发服务器的名称
                Audience = audience, // 指定 Token 接收者
                SigningCredentials = _tokenOptions.Credentials,
                Subject = identity,
                Expires = expire
            });
              
            return token;
        }

        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="user">用户登录信息</param>
        /// <param name="audience">要访问的网站</param>
        /// <returns></returns>
        [HttpPost("ManagerLogin")]
        public string ManagerLogin([FromBody]LoginUser user)
        {
            DateTime expire = DateTime.Now.AddDays(1);
            // 在这里来验证用户的用户名、密码
            UserManage.LoginResultEntity result = this.HttpContext.UserLogin(userClient,   userFunctionClient, c =>
            {
                return CreateToken(c, expire, ApplicationConfig.ManagerAudience);
            }, user.Username, user.Password, "", UserManage.LoginSource.Manager);

            return JsonHelper.ToJson(result); 
        }
        /// <summary>
        /// 后端 用户ID自动登录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ManagerPrimaryLogin/{userId}")]
        public string ManagerPrimaryLogin(int userId)
        {
            DateTime expire = DateTime.Now.AddDays(1);
            // 在这里来验证用户的用户名、密码
            UserManage.LoginResultEntity result = this.HttpContext.UserLogin(userClient,   userFunctionClient, c =>
            {
                return CreateToken(c, expire, ApplicationConfig.ClientAudience);
            }, "", "", "", UserManage.LoginSource.Manager, userId);

            return JsonHelper.ToJson(result);
        }


        /// <summary>
        /// 前端 用户登录
        /// </summary>
        /// <param name="user">用户登录信息</param>
        /// <param name="audience">要访问的网站</param>
        /// <returns></returns>
        [HttpPost("ClientLogin")]
        public string ClientLogin([FromBody]LoginUser user)
        {
            DateTime expire = DateTime.Now.AddDays(1);
            // 在这里来验证用户的用户名、密码
            UserManage.LoginResultEntity result = this.HttpContext.UserLogin(userClient,  userFunctionClient, c =>
            {
                return CreateToken(c, expire, ApplicationConfig.ClientAudience);
            }, user.Username, user.Password, "", UserManage.LoginSource.Client);

            return JsonHelper.ToJson(result);
        }

        /// <summary>
        /// 前端 用户ID自动登录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ClientPrimaryLogin/{userId}")]
        public string ClientPrimaryLogin(int userId)
        {
            DateTime expire = DateTime.Now.AddDays(1);
            // 在这里来验证用户的用户名、密码
            UserManage.LoginResultEntity result = this.HttpContext.UserLogin(userClient, userFunctionClient, c =>
            {
                return CreateToken(c, expire, ApplicationConfig.ClientAudience);
            },"", "", "", UserManage.LoginSource.Client, userId);

            return JsonHelper.ToJson(result);
        }

        /// <summary>
        /// App 移动端用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("AppLogin")]
        public string AppLogin([FromBody]LoginUser user)
        {
            AppLoginResultEntity appLoginResultEntity = new AppLoginResultEntity();
            DateTime expire = DateTime.Now.AddDays(100);
            // 在这里来验证用户的用户名、密码
            LoginResultEntity result = this.HttpContext.UserLogin(userClient,  userFunctionClient, c =>
            { 
                appLoginResultEntity.appCurrentUserEntity.AppUser = new AppUserCurrentUserEntity
                {
                    ID = c.ID,
                    IS_MAIN = c.IS_MAIN,
                    USER = c.USER,
                    USER_NAME = c.USER_NAME
                }; 

                appLoginResultEntity.appCurrentUserEntity.ID = c.ID;
                appLoginResultEntity.appCurrentUserEntity.USER = c.USER;

                return CreateToken(appLoginResultEntity.appCurrentUserEntity, expire, ApplicationConfig.AppAudience);
            }, user.Username, user.Password, "", UserManage.LoginSource.Client);

            appLoginResultEntity.loginResultEntity = result;
            return JsonHelper.ToJson(appLoginResultEntity);
        }
    }
}
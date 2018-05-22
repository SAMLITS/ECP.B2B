using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Entity
{
    public class LoginEntity
    {
        public LoginEntity()
        {
        }
        public LoginEntity(decimal? _userId)
        {
            userId = _userId;
        }

        public LoginEntity(string _uName, string _uPass)
        {
            uName = _uName;
            uPass = _uPass;

            if (string.IsNullOrEmpty(uName) || string.IsNullOrEmpty(uPass))
            {
                throw new Exception("必须输入用户名密码！");
            }
        }

        /// <summary>
        /// 用户id 如果传入此值的话会直接对这个用户进行授权，不会进行其他校验
        /// </summary>
        public decimal? userId;

        public string uName;
        public string uPass;
        public LoginType loginType;
        public bool isNotPwd = false;
        public string cookie_ati;

        public string ClientSourceDomain;
        public string ClientSourceVpnType;
        public string ClientSourceDomainType;   //VPN-专网 / PRT-私网
        public string ClientRequestIp;
        public string ClientRequestAddressIp;
    }


    public class LoginResult
    {
        public LoginResult() { }
        public LoginResult(LoginStatus _status, string _resultMsg)
        {
            this.Status = _status;
            this.ResultMsg = _resultMsg;
        }

        public LoginResult(string _resultCode, LoginStatus _status)
        {
            this.ResultCode = _resultCode;
            this.Status = _status;
        }
        public LoginResult(LoginStatus _status, RedirectType _redirectType, string _redirectUrl = "")
        {
            this.Status = _status;
            this.RedirectType = _redirectType;
            this.RedirectUrl = _redirectUrl;
        }


        /// <summary>
        /// 登录结果状态
        /// </summary>
        public LoginStatus Status { get; set; }

        /// <summary>
        /// 输出消息
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        /// 消息Code
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// 重定向类型
        /// </summary>
        public RedirectType RedirectType { get; set; }
        /// <summary>
        /// 重定向URL
        /// </summary>
        public string RedirectUrl { get; set; }

        public Dictionary<string, object> SessionDict { get; set; } = new Dictionary<string, object>();
        public object PrincipalUser;


        /// <summary>
        /// 登录成功用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 当前交易方配置密钥   返回时会清空
        /// </summary>
        public string Atribute1 { get; set; }
        /// <summary>
        /// 当前专网标识  返回时会清空
        /// </summary>
        public string Atribute2 { get; set; }

        /// <summary>
        /// 当前登录用户 同ECP 登录 Session UserInfo 对象一直    返回时会清空
        /// </summary>
        public string User { get; set; }
    }

    public enum LoginStatus
    {
        Success,
        Error,
        UserNameOrPwdError
    }

    public enum RedirectType
    {
        BidSuccess,
        EcpSuccess,
        CheckCodeError
    }

    public enum LoginType
    {
        ECP,
        BID,
        B2B
    }
}

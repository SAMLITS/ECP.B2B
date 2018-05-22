using ECP.Util.ConfigDc.GrpcClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.Common
{
    public class ApplicationConfig
    {
        /// <summary>
        /// 前端登录授权地址
        /// </summary>
        public static string ClientLoginTokenAddress;
        /// <summary>
        /// 前端根据用户ID自动登录授权地址
        /// </summary>
        public static string ClientPrimaryLoginTokenAddress;
        
        /// <summary>
        /// 后端登录授权地址
        /// </summary>
        public static string ManagerLoginTokenAddress;
        /// <summary>
        /// 后端根据用户ID自动登录授权地址
        /// </summary>
        public static string ManagerPrimaryLoginTokenAddress;
        
        /// <summary>
        /// APP端登录授权地址
        /// </summary>
        public static string AppLoginTokenAddress;
        

        /// <summary>
        /// 前台文件上传地址
        /// </summary>
        public static string ClientFileUploadDomain;
        /// <summary>
        /// 后台文件上传地址
        /// </summary>

        public static string ManagerFileUploadDomain;

        /// <summary>
        /// 对称加密Key
        /// </summary>
        public static string EncryptKey;

        /// <summary>
        /// 服务器token签发者
        /// </summary>
        public static string Issuer;
        /// <summary>
        /// 后台token接收者
        /// </summary>
        public static string ManagerAudience;
        /// <summary>
        /// 前台token接收者
        /// </summary>
        public static string ClientAudience;
        /// <summary>
        /// App端token接收者
        /// </summary>
        public static string AppAudience;


        /// <summary>
        /// 客户端应用程序名称
        /// </summary>
        public static string ClientAppName;
        /// <summary>
        /// 客户端共享配置秘钥
        /// </summary>
        public static string ClientPersistKeyFilePath;
        /// <summary>
        /// 客户端主域名Domain  比如：ecp56.com
        /// </summary>
        public static string ClientMainDomain;

        /// <summary>
        /// ECP SDK API 站点Domain地址
        /// </summary>
        public static string Ecp_b2b_sdk_api_domain;
        /// <summary>
        /// ECP SDK API 数据传输过程加密秘钥
        /// </summary>
        public static string Ecp_b2b_sdk_api_secretKey;
        /// <summary>
        /// ECP  切换地址
        /// </summary>
        public static string Ecp_open_domain_address;


        static ApplicationConfig()
        {
            ClientLoginTokenAddress = ConfigDcUtilClientImp.GetApplicationConfig("ClientLoginTokenAddress").Result;
            ClientPrimaryLoginTokenAddress = ConfigDcUtilClientImp.GetApplicationConfig("ClientPrimaryLoginTokenAddress").Result;
            

            ManagerLoginTokenAddress = ConfigDcUtilClientImp.GetApplicationConfig("ManagerLoginTokenAddress").Result;
            AppLoginTokenAddress = ConfigDcUtilClientImp.GetApplicationConfig("AppLoginTokenAddress").Result;

            ClientFileUploadDomain = ConfigDcUtilClientImp.GetApplicationConfig("ClientFileUploadDomain").Result;
            ManagerFileUploadDomain = ConfigDcUtilClientImp.GetApplicationConfig("ManagerFileUploadDomain").Result;

            EncryptKey = ConfigDcUtilClientImp.GetApplicationConfig("EncryptKey").Result;
             
            Issuer = ConfigDcUtilClientImp.GetApplicationConfig("Issuer").Result;
            ManagerAudience = ConfigDcUtilClientImp.GetApplicationConfig("ManagerAudience").Result;
            ClientAudience = ConfigDcUtilClientImp.GetApplicationConfig("ClientAudience").Result;
            AppAudience = ConfigDcUtilClientImp.GetApplicationConfig("AppAudience").Result;


            ClientAppName = ConfigDcUtilClientImp.GetApplicationConfig("ClientAppName").Result;
            ClientPersistKeyFilePath = ConfigDcUtilClientImp.GetApplicationConfig("ClientPersistKeyFilePath").Result;
            ClientMainDomain = ConfigDcUtilClientImp.GetApplicationConfig("ClientMainDomain").Result;


            Ecp_b2b_sdk_api_domain = ConfigDcUtilClientImp.GetApplicationConfig("Ecp_b2b_sdk_api_domain").Result;
            Ecp_b2b_sdk_api_secretKey = ConfigDcUtilClientImp.GetApplicationConfig("Ecp_b2b_sdk_api_secretKey").Result;
            Ecp_open_domain_address = ConfigDcUtilClientImp.GetApplicationConfig("Ecp_open_domain_address").Result;

            ManagerPrimaryLoginTokenAddress = ConfigDcUtilClientImp.GetApplicationConfig("ManagerPrimaryLoginTokenAddress").Result;
            
        }
    }
}

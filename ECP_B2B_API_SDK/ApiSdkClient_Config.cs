using System;

namespace ECP_B2B_API_SDK
{
    /// <summary>
    /// SDK 客户端 需要在Global中进行指定的 全局配置
    /// 
    /// 另：需要在Startup.cs的ConfigureServices方法中：services.AddHttpContextAccessor();
    ///                      Configure方法中：app.UseStaticHttpContext();
    /// </summary>
    public class ApiSdkClient_Config
    {
        /// <summary>
        /// 当前平台配置密钥key
        /// </summary>
        public static Func<string> ClientSecretKey;
        /// <summary>
        /// 服务端API Domain 地址
        /// </summary>
        public static string ServerAppDomain;

        /// <summary>
        /// ECP 后端 切换地址
        /// </summary>
        public static Func<string> EcpB2bOpenDomainAddress;

        /// <summary>
        /// ECP B2B 后端 切换地址     可不用赋值
        /// </summary>
        public static Func<string> EcpB2bManagerOpenDomainAddress;

        /// <summary>
        /// 获取用户唯一标识方式
        /// </summary>
        public static Func<object> GetUserUniqueFunc;
    }
}

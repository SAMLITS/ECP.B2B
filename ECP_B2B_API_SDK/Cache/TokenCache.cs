using ECP_B2B_API_SDK.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECP_B2B_API_SDK.Cache
{
    /// <summary>
    /// token 用户信息记录   仅限客户端使用(服务器端不可调用)
    /// </summary>
    public class TokenCache
    {
        static TokenCache()
        {
            AccessToKenInit();
        }
        private static object objLock = new object();
        private static Dictionary<string, string> AccessToKenDict = new Dictionary<string, string>();

        /// <summary>
        ///  从持久化文件中初始化字典
        /// </summary>
        private static void AccessToKenInit()
        {
            lock (objLock)
            {
                var jsonStr = FileHelper.FileReadAllText("ecp_b2b_api_builder/accessTokenInfo.lib");
                if (jsonStr != null)
                {
                    AccessToKenDict = JsonHelper.ToObject<Dictionary<string, string>>(jsonStr);
                }
            }
        }
        /// <summary>
        /// 缓存持久化记录信息
        /// </summary>
        private static void AccessToKenWrite()
        {
            lock (objLock)
            {
                var jsonStr = JsonHelper.ToJson(AccessToKenDict);
                FileHelper.AddTextFile("ecp_b2b_api_builder/accessTokenInfo.lib", jsonStr);
            }
        }


        /// <summary>
        /// 设置/更新 token 信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        public static void SetAccessToKen(string userName,string token)
        {
            if(IsExistsAccessToken(userName))
            {
                AccessToKenDict[userName] = token;
            }
            else
            {
                AccessToKenDict.Add(userName, token);
            }
            AccessToKenWrite();
        }
        /// <summary>
        /// 获取指定用户的token信息
        /// </summary>
        /// <param name="userName"></param>
        public static string GetAccessToKen(string userName)
        {
            AccessToKenDict.TryGetValue(userName, out string token);
            return token;
        }
        /// <summary>
        /// 是否存在用户对应token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsExistsAccessToken(string userName)
        {
            return AccessToKenDict.Keys.Contains(userName);
        } 
    }
}

using ECP_B2B_API_SDK.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECP_B2B_API_SDK.Cache
{
    /// <summary>
    /// 服务器端 平台专网类型 密钥 缓存
    /// </summary>
    public class ServerSecretKeyCache
    {
        static ServerSecretKeyCache()
        {
            VpnTypeInit();
        }
        private static object objLock = new object();
        private static Dictionary<string, string> VpnTypeSecretKeyDict = new Dictionary<string, string>();
        /// <summary>
        /// 从持久化文件中初始化字典
        /// </summary>
        private static void VpnTypeInit()
        {
            lock (objLock)
            {
                var jsonStr = FileHelper.FileReadAllText("ecp_b2b_api_builder/vpnTypeSecretKeyInfo.lib");
                if (jsonStr != null)
                {
                    VpnTypeSecretKeyDict = JsonHelper.ToObject<Dictionary<string, string>>(jsonStr);
                }
            }
        }
        /// <summary>
        /// 缓存持久化记录信息
        /// </summary>
        private static void VpnTypeWrite()
        {
            lock (objLock)
            {
                var jsonStr = JsonHelper.ToJson(VpnTypeSecretKeyDict);
                FileHelper.AddTextFile("ecp_b2b_api_builder/vpnTypeSecretKeyInfo.lib", jsonStr);
            }
        }

        /// <summary>
        /// 设置/更新 专网密钥配置 信息
        /// </summary>
        /// <param name="vpnType"></param>
        /// <param name="key"></param>
        public static void SetSecretKey(string vpnType, string key)
        {
            if (IsExistsSecretKey(vpnType))
            {
                VpnTypeSecretKeyDict[vpnType] = key;
            }
            else
            {
                VpnTypeSecretKeyDict.Add(vpnType, key);
            }
            VpnTypeWrite();
        }
        /// <summary>
        /// 获取 专网密钥配置 信息
        /// </summary>
        /// <param name="vpnType"></param>
        public static string GetSecretKey(string vpnType)
        {
            VpnTypeSecretKeyDict.TryGetValue(vpnType, out string key);
            return key;
        }
        /// <summary>
        /// 是否存在 专网密钥配置 信息
        /// </summary>
        /// <param name="vpnType"></param>
        /// <returns></returns>
        public static bool IsExistsSecretKey(string vpnType)
        {
            return VpnTypeSecretKeyDict.Keys.Contains(vpnType);
        }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.Manager.Models
{
    public static class AppContextConfigInfo
    {
        /// <summary>
        /// 系统版本
        /// </summary>
        public static string Version; 

        /// <summary>
        /// 配置初始化
        /// </summary>
        /// <param name="Configuration"></param>
        public static void InitAppConfigInfo(IConfiguration Configuration)
        {
            IConfiguration appSettingsSection =  Configuration.GetSection("AppSettings");
            Version = appSettingsSection["Version"]; 
        }
    }
}

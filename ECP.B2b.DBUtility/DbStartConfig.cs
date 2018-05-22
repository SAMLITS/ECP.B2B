using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DBUtility
{
    /// <summary>
    /// 外部程序启动时赋值 Global 或 Startup 中
    /// </summary>
    public class DbStartConfig
    {
        /// <summary>
        /// Oracle数据库链接地址
        /// </summary>
        public static string DbOracleConnectionString;
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Helper
{
    public class LogHelper
    {
        private static string LogPath = "ecp_b2b_api_builder/logs";
        private static string LogPath2 = System.AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 写入日志文件
        /// </summary>
        /// <param name="text"></param>
        private static object objLock = new object();
        public static void WriteLog(string text)
        {
            string savePath = Path.Combine(LogPath2, LogPath, DateTime.Now.ToString("yyyy-MM-dd"));
            string saveFile = Path.Combine(savePath, DateTime.Now.Hour + ".log");
            lock (objLock)
            {
                if (Directory.Exists(savePath))
                {
                    //文件不存在自动创建 
                    File.AppendAllText(saveFile, DateTime.Now.ToString("HH:mm:ss") + "：" + text + System.Environment.NewLine);
                }
                else
                {
                    Directory.CreateDirectory(savePath);
                    WriteLog(text);
                }
            }
        } 
    }
}

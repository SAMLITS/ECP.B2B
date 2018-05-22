using System;
using System.IO;
using System.Diagnostics;
using log4net.Config;
using log4net; 
using System.Collections.Generic;

namespace ECP.Util.Common
{ 
    public class Logger 
    {
        static Logger()
        {
            XmlConfigurator.Configure(LogManager.CreateRepository("LogRepository"), new FileInfo(Path.Combine("cfgFiles\\log4net.cfg.xml")));
        }

        private ILog loger = null;
        public Logger(Type type)
        {
            loger = LogManager.GetLogger("LogRepository", type); 
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Error(string msg = "出现异常", Exception ex = null)
        {
            loger.Error(msg, ex); 
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Warn(string msg)
        {
            loger.Warn(msg); 
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            loger.Info(msg); 
        }

        /// <summary>
        /// Log4日志
        /// </summary>
        /// <param name="msg"></param>
        public void Debug(string msg)
        {
            loger.Debug(msg);
        }
    }
}

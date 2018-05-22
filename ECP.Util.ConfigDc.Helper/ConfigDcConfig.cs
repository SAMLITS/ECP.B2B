using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace ECP.Util.ConfigDc.Helper
{
    /// <summary>
    /// 配置中心的相关配置
    /// </summary>
    public class ConfigDcConfig
    {
        private static JObject dbConfigObj;

        static ConfigDcConfig()
        {
            string content = File.ReadAllText("cfgFiles\\bindAddressServices.json", Encoding.UTF8);
            dbConfigObj = (JObject)JsonConvert.DeserializeObject(content);
        }

        public static void  GetServerName(out string serverName)
        {
            serverName = dbConfigObj.Value<string>("serverName");
        }

        public static void GetDcAddress(out string addressVal,out string ip,out int port )
        {
            addressVal =  dbConfigObj.Value<string>("dcAddress");
            var array = addressVal.Split(':');
            ip = array[0];
            port = Convert.ToInt32(array[1].Trim());
        }

        public static void GetServerAddress(string addressCode,out string addressVal)
        {
            addressVal = dbConfigObj["serverAddress"].Value<string>(addressCode);
        }
        public static void GetServerAddress(string addressCode, out string addressVal, out string ip, out int port)
        {
            addressVal = dbConfigObj["serverAddress"].Value<string>(addressCode);
            var array = addressVal.Split(':');
            ip = array[0];
            port = Convert.ToInt32(array[1].Trim());
        }
    }
}

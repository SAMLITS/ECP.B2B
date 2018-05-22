using ECP.Util.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.ConfigDc.GrpcServer
{
    public class ReadConfigStatic
    {
        public static JObject dbConfigObj;
        public static JObject serviceRegObj;
        public static JObject applicationConfigObj;

        static ReadConfigStatic()
        {
            dbConfigObj = JsonHelper.GetJObjectByPath("cfgFiles/dbConfig.json");
            serviceRegObj = JsonHelper.GetJObjectByPath("cfgFiles/serviceRegiser.json");
            applicationConfigObj = JsonHelper.GetJObjectByPath("cfgFiles/applicationConfig.json");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ECP.Util.Common
{
    public class JsonHelper
    {
        #region Json  
        /// <summary>
        /// JsonConvert.SerializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// JsonConvert.DeserializeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content)
        {   
            return JsonConvert.DeserializeObject<T>(content);
        }
         
        public static JObject GetJObjectByPath(string path) 
        {
            string content = FileHelper.FileReadAllText(path);
            return  (JObject)JsonConvert.DeserializeObject(content);
        }

        public static JObject GetJObjectByContent(string content)
        {
            return (JObject)JsonConvert.DeserializeObject(content);
        }
        

        #endregion Json

    }
}

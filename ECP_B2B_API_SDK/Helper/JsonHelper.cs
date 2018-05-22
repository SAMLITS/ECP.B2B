using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Helper
{
   public class JsonHelper
    { 
        /// <summary>
        /// 实体转json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new DecimalDoubleConverter()); 
        }

        /// <summary>
        /// json字符串转泛型实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ToObject<T>(string content)
        { 
            return JsonConvert.DeserializeObject<T>(content);  //, new DecimalDoubleConverter()
        }
    }
}

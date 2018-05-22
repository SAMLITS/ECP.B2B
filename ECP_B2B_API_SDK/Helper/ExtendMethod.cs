using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Helper
{
    public static class ExtendMethod
    {
        /// <summary>
        /// 统一ToString调用方法 
        /// </summary>
        /// <param name="obj">需要转换为字符串的参数</param>
        /// <returns>""或转化结果</returns>
        public static string DBToString(this object obj)
        {
            if (obj is DBNull || obj == null)
                return "";
            else
                return obj.ToString();
        }

        /// <summary>
        /// dynamic 动态类型转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T DynamicToObject<T>( dynamic source)
        {
            var jsonStr = JsonHelper.ToJson(source);
            return JsonHelper.ToObject<T>(jsonStr);
        }

        /// <summary>
        /// 分页对象转换为字典对象
        /// 
        /// 分页相关：PageInfo
        /// 条件相关：QueryParams
        /// </summary>
        /// <param name="pageObj"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, object>> ToApiPageDict(this object pageObj)
        {
            var objType = pageObj.GetType();

            //分页参数
            var pageInfoObj = objType.GetProperty("PageInfo").GetValue(pageObj);
            Dictionary<string, object> pageInfoDict = new Dictionary<string, object>();
            var pageInfoType = pageInfoObj.GetType();
            var pageProperties = pageInfoType.GetProperties();
            for (int i = 0; i < pageProperties.Length; i++)
            {
                pageInfoDict.Add(pageProperties[i].Name, pageProperties[i].GetValue(pageInfoObj));
            }

            var queryParamsObj = objType.GetProperty("QueryParams").GetValue(pageObj);
            Dictionary<string, object> queryParamsDict = new Dictionary<string, object>();
            var queryParamsType = queryParamsObj.GetType();
            var queryProperties = queryParamsType.GetProperties();

            //条件参数
            var queryPropts = queryProperties.Where(p => !p.Name.Contains("IS_LIKE_")).ToArray();
            for (int i = 0; i < queryPropts.Length; i++)
                queryParamsDict.Add(queryPropts[i].Name, queryPropts[i].GetValue(queryParamsObj));

            //模糊参数
            var likePropts = queryProperties.Where(p => p.Name.Contains("IS_LIKE_")).ToArray();
            for (int i = 0; i < likePropts.Length; i++)
            {
                if (Convert.ToBoolean(likePropts[i].GetValue(queryParamsObj)))
                {
                    var fieldName = likePropts[i].Name.Substring(8);
                    if (queryParamsDict.ContainsKey(fieldName))
                    {
                        var fieldValue = queryParamsDict[fieldName];
                        if (!string.IsNullOrEmpty(fieldValue.DBToString()))
                        {
                            queryParamsDict[fieldName] = "%" + fieldValue.DBToString() + "%";
                        }
                    }
                }
            }

            return new Dictionary<string, Dictionary<string, object>>
            {
                {"PageInfo" , pageInfoDict },
                {"QueryParams" , queryParamsDict }
            };
        }


        /// <summary>
        /// 将JArray对象转换为List<object>类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<object> JArrayToListObj(this object obj)
        {
            if (obj != null)
                return ((Newtonsoft.Json.Linq.JArray)obj).ToObject<List<object>>();
            return null;
        }
        /// <summary>
        /// 将JArray对象返回为sql in 的格式  
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>比如：'111','333','444'</returns>
        public static string JArrayToInSql(this object obj)
        {
            if (obj != null)
            {
                var listObj = obj.JArrayToListObj();
                if (listObj.Count == 0)
                    return "";
                return string.Join(",", listObj.Where(s => !string.IsNullOrEmpty(s.DBToString().Trim())).Select(s => "'" + s.DBToString() + "'").ToArray());
            }
            return null;
        }
        public static string JArrayToInSql<T>(this List<T> objList)
        {
            if (objList != null)
                return string.Join(",", objList.Where(s => !string.IsNullOrEmpty(s.DBToString().Trim())).Select(s => "'" + s.DBToString() + "'").ToArray());
            return null;
        }
    }
}

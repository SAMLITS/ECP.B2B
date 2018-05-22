using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECP_B2B_API_SDK.Helper
{ 
    public class DESEncrypt
    {
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(Text);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(sKey);
            byte[] allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return string.Join(".", resultArray) + "..sec_info";
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length); 
        }


        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            Text = Text.Replace("..sec_info", "");
            var bytes = Text.Split('.');
            byte[] inputArray = Array.ConvertAll(bytes, s => Convert.ToByte(s));
            //byte[] inputArray = Convert.FromBase64String(Text); 

            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(sKey);
            byte[] allKey = new byte[24];
            Buffer.BlockCopy(byteKey, 0, allKey, 0, 16);
            Buffer.BlockCopy(byteKey, 0, allKey, 16, 8);
            tripleDES.Key = allKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

    }


    public class UrlParamAddress
    {
        public UrlParamAddress(string _UrlAddressName, Dictionary<string, string> _Params)
        {
            UrlAddressName = _UrlAddressName;
            Params = _Params;
        }
        public UrlParamAddress(string _UrlAddressName, Dictionary<string, string> _Params, Dictionary<string, string> _StaticParams)
        {
            UrlAddressName = _UrlAddressName;
            Params = _Params;
            StaticParams = _StaticParams;
        }

        /// <summary>
        /// 地址名称  dt 列名
        /// </summary>
        public string UrlAddressName { get; set; }

        /// <summary>
        /// 数据集绑定参数动态指定
        /// </summary>
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 固定值参数
        /// </summary>
        public Dictionary<string, string> StaticParams { get; set; } = new Dictionary<string, string>();
    }

    public class SecretBuilderConfig
    {
        public SecretBuilderConfig(SecretConfigSource _secretConfigSource = SecretConfigSource.Default,string _saltValue = "")
        {
            if (_secretConfigSource == SecretConfigSource.Default)
            {
                saltValue = "XXXXXX-XXXX-XXX";
            }
            else if (_secretConfigSource == SecretConfigSource.PublicOpen)
            {
                saltValue = _saltValue;
            }
        }

        public SecretConfigSource secretConfigSource;
        public string saltValue;
    }
    public enum SecretConfigSource
    {
        /// <summary>
        /// 默认
        /// 未登录状态下使用加密方式
        /// </summary>
        Default,
        /// <summary>
        /// token登录验证后的公共对外加密方式
        /// </summary>
        PublicOpen
    }

    public class EncryptDataTableEntity
    {
        public List<UrlParamAddress> urlList;
        public string[] colsName;
    }

    /// <summary>
    /// 数据传输加解密 扩展
    /// </summary>
    public class SecretDesEncryptHelper
    {

        /// <summary>
        /// 默认配置
        /// </summary>
        public static SecretDesEncryptHelper secretDefaultEncryptHelper = new SecretDesEncryptHelper(new SecretBuilderConfig());

        private SecretBuilderConfig secretBuilderConfig;
        public SecretDesEncryptHelper(SecretBuilderConfig _secretBuilderConfig)
        {
            secretBuilderConfig = _secretBuilderConfig;
        }

        /// <summary>
        /// 在客户端创建SecretDesEncryptHelper对象
        /// </summary>
        /// <returns></returns>
        public static SecretDesEncryptHelper CreateClientCurrentObject()
        {
            return new SecretDesEncryptHelper(new SecretBuilderConfig(SecretConfigSource.PublicOpen, ApiSdkClient_Config.ClientSecretKey()));
        }
        /// <summary>
        /// 在服务器端创建SecretDesEncryptHelper对象
        /// </summary>
        /// <param name="vpnType"></param>
        /// <returns></returns>
        public static SecretDesEncryptHelper CreateServerCurrentObject(string vpnType)
        {
            return new SecretDesEncryptHelper(new SecretBuilderConfig(SecretConfigSource.PublicOpen, Cache.ServerSecretKeyCache.GetSecretKey(vpnType)));
        }

        #region DataTable 加解密相关
        public DataTable EncryptTableColumn(DataTable dt, EncryptDataTableEntity encryptDataTableEntity)
        {
            var urlList = encryptDataTableEntity.urlList;
            var colsName = encryptDataTableEntity.colsName;

            string saltValue = secretBuilderConfig.saltValue;

            DataTable dtResult = new DataTable();
            dtResult = dt.Clone();
            for (int i = 0; i < colsName.Length; i++)
            {
                dtResult.Columns.Add(colsName[i] + "_D");
            }
            if (urlList != null && urlList.Count > 0)
            {
                for (int i = 0; i < urlList.Count; i++)
                {
                    dtResult.Columns.Add(urlList[i].UrlAddressName + "_ADDRESSURL");
                }
            }

            //将所有字段均改为string类型 
            foreach (DataColumn col in dtResult.Columns)
            {
                col.DataType = typeof(String);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow rowNew = dtResult.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    if (colsName.Contains(dt.Columns[j].ColumnName))
                    {
                        //删除未加密列时需注释该段代码
                        rowNew[j] = dt.Rows[i][j];
                        rowNew[dt.Columns[j].ColumnName + "_D"] = DESEncrypt.Encrypt(dt.Rows[i][j].DBToString(), saltValue);
                    }
                    else
                    {
                        rowNew[j] = dt.Rows[i][j];
                    }
                }
                dtResult.Rows.Add(rowNew);
            }

            if (urlList != null && urlList.Count > 0)
            {
                foreach (DataRow row in dtResult.Rows)
                {
                    //给地址列赋值
                    for (int i = 0; i < urlList.Count; i++)
                    {
                        string urlListParamsJson = JsonHelper.ToJson(urlList[i].Params);
                        Dictionary<string, string> copyParams = JsonHelper.ToObject<Dictionary<string, string>>(urlListParamsJson);
                        foreach (var key in urlList[i].Params.Keys)
                        {
                            copyParams[key] = row[copyParams[key]].DBToString();
                        }
                        //写入固定值
                        foreach (var sKey in urlList[i].StaticParams.Keys)
                        {
                            copyParams.Add(sKey, urlList[i].StaticParams[sKey]);
                        }

                        var urlAddress = GetBuilderEncryptUrl(copyParams);
                        row[urlList[i].UrlAddressName + "_ADDRESSURL"] = urlAddress;
                    }
                }
            }

            return dtResult;
        }

        /// <summary>
        /// 对DataTable中的指定字段进行加密   使用默认配置
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="context"></param>
        /// <param name="colsName"></param>
        /// <returns></returns>
        public DataTable EncryptTableColumn(DataTable dt, List<UrlParamAddress> urlList, params string[] colsName)
        {
            //默认配置 
            var dtResult = EncryptTableColumn(dt, new EncryptDataTableEntity
            {
                colsName = colsName,
                urlList = urlList
            });
            return dtResult;
        }
        public DataTable EncryptTableColumn(DataTable dt, UrlParamAddress urlParam, params string[] colsName)
        {
            return EncryptTableColumn(dt, new List<UrlParamAddress> { urlParam }, colsName);
        }
        public DataTable EncryptTableColumn(DataTable dt, params string[] colsName)
        {
            return EncryptTableColumn(dt, new List<UrlParamAddress> { }, colsName);
        }

        #endregion

        #region 普通字段加解密调用方法
        /// <summary>
        /// 对字符串进行解密
        /// 
        /// 字符串解密 可同时通过“,”隔开传入多个解密字符串，解密后再按","隔开方法返回整个字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public T GetDecryptValue<T>(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return default(T);

            string saltValue = secretBuilderConfig.saltValue;
            var paramValues = paramValue.Split(',');
            string[] returnParams = new string[paramValues.Length];
            for (int i = 0; i < paramValues.Length; i++)
            {
                if (
                      string.IsNullOrEmpty(paramValues[i])
                      ||
                      !paramValues[i].Contains("..sec_info")
                      )
                    returnParams[i] = paramValues[i];
                else
                    returnParams[i] = DESEncrypt.Decrypt(paramValues[i], saltValue);
            }
            paramValue = string.Join(",", returnParams);
            return (T)Convert.ChangeType(paramValue, typeof(T));
        }
        /// <summary>
        /// 对数据进行加密
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public string SetEncryptValue(object paramValue)
        {
            if (paramValue.DBToString() == "")
                return "";

            string saltValue = secretBuilderConfig.saltValue;
            string encryptValue = DESEncrypt.Encrypt(paramValue.DBToString(), saltValue);
            return encryptValue;
        }

        #endregion

        #region 对象经过json到加密字符串->加密字符串解密经过json到对象  相互之前转换
        public string SetEncryptObject<T>(T tModel)
        {
            var jsonModel = JsonHelper.ToJson(tModel);
            return SetEncryptValue(jsonModel);
        }
        public T GetDecryptObject<T>(string encryptStr)
        {
            if (encryptStr.IndexOf("?") == 0)
                encryptStr = encryptStr.Substring(1);
            var jsonModel = GetDecryptValue<string>(encryptStr);
            if (jsonModel.IndexOf("[") == 0)
                jsonModel = JsonListDecrypt(jsonModel);
            else
                jsonModel = JsonDecrypt(jsonModel);

            var tModel = JsonHelper.ToObject<T>(jsonModel);
            return tModel;
        }

        public string SetEncryptDicObject(Dictionary<string,object> dict)
        { 
            return SetEncryptObject(dict);
        }
        public Dictionary<string, object> GetDecryptDicObject(string encryptStr)
        { 
            return GetDecryptObject<Dictionary<string, object>>(encryptStr);
        }
        #endregion

        #region Json字符串解密
        /// <summary>
        /// 对json 字符串中的加密字段进行解密，并返回新的json字段
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string JsonDecrypt(string json)
        {
            JObject jo = JsonHelper.ToObject<JObject>(json);
            jo.Properties().ToList().ForEach(j =>
            {
                if (j.Value.Type == JTokenType.Array)
                {
                    jo[j.Name] = JsonHelper.ToObject<JArray>(JsonListDecrypt(j.Value.DBToString()));
                }
                else if (j.Value.Type == JTokenType.Object)
                {
                    jo[j.Name] = JsonHelper.ToObject<JObject>(JsonDecrypt(j.Value.DBToString())); ;
                }
                else
                {
                    jo[j.Name] = GetDecryptValue<string>(j.Value.DBToString());
                }
            });
            return JsonHelper.ToJson(jo);
        }
        public string JsonListDecrypt(string jsonList)
        {
            var jArray = JsonHelper.ToObject<JArray>(jsonList);
            if (jArray.Count > 0 && jArray[0].Type == JTokenType.Object)
            {
                //集合object类型 [{'a':'aaa'},{'a':'aaa'}]
                List<JObject> joList = JsonHelper.ToObject<List<JObject>>(jsonList);
                joList.ForEach(jo =>
                {
                    jo.Properties().ToList().ForEach(j =>
                    {
                        if (j.Value.Type == JTokenType.Array)
                        {
                            jo[j.Name] = JsonListDecrypt(j.Value.DBToString());
                        }
                        else if (j.Value.Type == JTokenType.Object)
                        {
                            jo[j.Name] = JsonDecrypt(j.Value.DBToString());
                        }
                        else
                        {
                            jo[j.Name] = this.GetDecryptValue<string>(j.Value.DBToString());
                        }
                    });
                });
                return JsonHelper.ToJson(joList);
            }
            else
            {
                //集合数值类型 [123,234,523] / ['123','adf','dss']
                var objList = JsonHelper.ToObject<List<object>>(jsonList);
                objList.ForEach(jo =>
                {
                    jo = this.GetDecryptValue<object>(jo.DBToString());
                });
                return JsonHelper.ToJson(objList);
            }
        }
        #endregion

        #region 其它帮助方法 统一生成加密 URL
        /// <summary>
        /// 统一生成加密 URL  格式：?xxxxx.xx   
        /// </summary>
        /// <returns></returns>
        public string GetBuilderEncryptUrl(Dictionary<string, string> urlListParams)
        {
            var saltValue = secretBuilderConfig.saltValue;

            string address = "?";
            string paramJson = JsonHelper.ToJson(urlListParams);
            address += DESEncrypt.Encrypt(paramJson, saltValue);

            return address;
        }

        /// <summary>
        /// 统一生成加密 URL  格式：?xxxxx.xx
        /// </summary>
        /// <returns></returns>
        public string GetBuilderEncryptUrl<T>(T tModel) where T : class
        {
            var saltValue = secretBuilderConfig.saltValue;
            string address = "?";
            string paramJson = JsonHelper.ToJson(tModel);
            address += DESEncrypt.Encrypt(paramJson, saltValue);
            return address;
        }
        #endregion

    }

    public class EcpB2bOpenChangeHelper
    {
        //// <summary>
        /// 生成跳转到 ECP B2B 的链接地址
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetOpenEcpB2bAddressUrl(object userId, string urladdress = "")
        {
            if (string.IsNullOrEmpty(urladdress))
                urladdress = ApiSdkClient_Config.EcpB2bOpenDomainAddress();
            urladdress = GetOpenPath(userId, urladdress);
            return urladdress;
        }
        public static string GetOpenEcpB2bManagerAddressUrl(object userId, string urladdress = "")
        {
            if (string.IsNullOrEmpty(urladdress))
                urladdress = ApiSdkClient_Config.EcpB2bManagerOpenDomainAddress();
            urladdress = GetOpenPath(userId, urladdress);
            return urladdress;
        }

        private static string GetOpenPath(object userId, string domainUrl)
        {
            var loginEnityJson = SecretDesEncryptHelper.secretDefaultEncryptHelper.SetEncryptDicObject(new Dictionary<string, object>
            {
                {"userId",userId},
                    { "dt",DateTime.Now.ToString()}
            });
            var urladdress = domainUrl + "?" + loginEnityJson;
            return urladdress;
        }
    }
}

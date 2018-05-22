using ECP_B2B_API_SDK.Cache;
using ECP_B2B_API_SDK.Entity;
using ECP_B2B_API_SDK.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 通过 实体/字典的方式进行调用
/// </summary>
namespace ECP_B2B_API_SDK.Helper
{

    public class RequestEntity
    {
        public void InitCurrentToKen()
        {
            var userName = ApiSdkClient_Config.GetUserUniqueFunc();
            token = TokenCache.GetAccessToKen(userName.DBToString());
        }
        public RequestEntity(string _url)
        {
            url = _url;
            InitCurrentToKen();
        } 
        public RequestEntity(object _bodyParams, bool _isAuthorization = true)
        {
            bodyParams = _bodyParams;
            isAuthorization = _isAuthorization;
            InitCurrentToKen();
        }
        public RequestEntity(object _bodyParams, string _url, bool _isAuthorization = true)
        {
            bodyParams = _bodyParams;
            url = _url;
            isAuthorization = _isAuthorization;
            InitCurrentToKen();
        }
         

        /// <summary>
        /// Dictionary<string, object>  OR  T实体
        /// </summary>
        public object bodyParams;
        public string url;
        public string token;
        public bool isAuthorization = true;
        public Action<HttpResponseResultModel> successCallback;
    }

    public class HttpClientHelper
    {
        private static string GetSendBeforeDesEncryptData(RequestEntity requestEntity)
        {
            if (requestEntity.isAuthorization)
                return SecretDesEncryptHelper.CreateClientCurrentObject().SetEncryptObject(requestEntity.bodyParams);
            else
                return SecretDesEncryptHelper.secretDefaultEncryptHelper.SetEncryptObject(requestEntity.bodyParams);
        }

        /// <summary>
        /// POST异步调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void SendPostAsync(RequestEntity requestEntity)
        {
            //对主体数据进行加密 
            var body = GetSendBeforeDesEncryptData(requestEntity);

            byte[] body_bytes = Encoding.UTF8.GetBytes(body);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiSdkClient_Config.ServerAppDomain + requestEntity.url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + requestEntity.token);
            request.Proxy = null;

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            request.BeginGetResponse(delegate (IAsyncResult ar)
            {
                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar); 
                using (StreamReader read = new StreamReader(response.GetResponseStream()))
                {
                    if (read.BaseStream != null)
                    {
                        var resModel = JsonHelper.ToObject<HttpResponseResultModel>(read.ReadToEnd());
                        requestEntity.successCallback(resModel); 
                    }
                } 
            }, request);

        }

        /// <summary>
        /// POST同步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static HttpResponseResultModel SendPostSync(RequestEntity requestEntity)
        {
            //对主体数据进行加密 
            var body = GetSendBeforeDesEncryptData(requestEntity);

            byte[] body_bytes = Encoding.UTF8.GetBytes(body);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiSdkClient_Config.ServerAppDomain + requestEntity.url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + requestEntity.token);
            request.Proxy = null;

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader read = new StreamReader(response.GetResponseStream()))
            {
                var resModel = JsonHelper.ToObject<HttpResponseResultModel>(read.ReadToEnd());
                return resModel;
            } 
        }


        /// <summary>
        /// GET同步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static HttpResponseResultModel SendGetSync(RequestEntity requestEntity)
        {
            //对主体数据进行加密 
            var body = GetSendBeforeDesEncryptData(requestEntity);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiSdkClient_Config.ServerAppDomain + requestEntity.url+"?"+ body);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + requestEntity.token);
            request.Proxy = null;

            //同步调用
            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 

            using (StreamReader read = new StreamReader(response.GetResponseStream()))
            {
                if (read.BaseStream != null )
                {
                    var resModel = JsonHelper.ToObject<HttpResponseResultModel>(read.ReadToEnd());
                    return resModel;
                }
            }
            return null;
        }

        /// <summary>
        /// GET异步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static void SendGetAsync(RequestEntity requestEntity)
        {
            //对主体数据进行加密 
            var body = GetSendBeforeDesEncryptData(requestEntity);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ApiSdkClient_Config.ServerAppDomain + requestEntity.url + "?" + body);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + requestEntity.token);
            request.Proxy = null;

            //异步调用
            request.BeginGetResponse(delegate (IAsyncResult ar)
            {
                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar); 
                using (StreamReader read = new StreamReader(response.GetResponseStream()))
                {
                    if (read.BaseStream != null )
                    {
                        var resModel = JsonHelper.ToObject<HttpResponseResultModel>(read.ReadToEnd());
                        requestEntity.successCallback(resModel);
                    }
                } 
            }, request);
        }
    }
}
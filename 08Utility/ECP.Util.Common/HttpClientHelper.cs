/*
 *创建人：LTS 
 *创建时间：2017-09-21
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ECP.Util.Common
{
    public class HttpClientHelper<T>
    {

        // //System.IO.IOException:Cannot close the stream until all bytes are written错误原因,：应指定Encoding.ASCII 
        /// <summary>
        /// POST异步调用WCF服务  
        /// read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void SendPostAsync(T jo, string url, Action<StreamReader> successCallback)
        {
            string body = JsonHelper.ToJson(jo);

            byte[] body_bytes = Encoding.UTF8.GetBytes(body.ToString());

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            request.BeginGetResponse(delegate (IAsyncResult ar)
            {

                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                StreamReader read = new StreamReader(response.GetResponseStream());

                //read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
                if (read.BaseStream != null && read.BaseStream.Length > 0)
                {
                    successCallback(read);

                    read.Close();
                }
            }, request);

        }

        /// <summary>
        /// POST同步调用WCF服务    通用，不管存不存在中文问题
        /// read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void SendPostSync(T jo, string url, Action<StreamReader> successCallback)
        {
            string body = JsonHelper.ToJson(jo);
            byte[] body_bytes = Encoding.UTF8.GetBytes(body);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader read = new StreamReader(response.GetResponseStream()))
            {
                //read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
                successCallback(read);
            }
        }



        /// <summary>
        /// GET同步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static void SendGetSync(string url, Action<StreamReader> successCallback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            //同步调用
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream());


            if (read.BaseStream != null)
            {
                successCallback(read);

                read.Close();
            }
        }

        /// <summary>
        /// GET异步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static void SendGetAsync(string url, Action<StreamReader> successCallback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            //异步调用
            request.BeginGetResponse(delegate (IAsyncResult ar)
            {
                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                StreamReader read = new StreamReader(response.GetResponseStream());

                if (read.BaseStream != null)
                {
                    successCallback(read);

                    read.Close();
                }
            }, request);
        }
    }

    public class HttpClientHelper
    {

        // //System.IO.IOException:Cannot close the stream until all bytes are written错误原因,：应指定Encoding.ASCII 
        /// <summary>
        /// POST异步调用WCF服务  
        /// read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void SendPostAsync(string url, string bodyContent, Action<StreamReader> successCallback)
        {
            byte[] body_bytes = Encoding.UTF8.GetBytes(bodyContent.ToString());
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            request.BeginGetResponse(delegate (IAsyncResult ar)
            {

                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                StreamReader read = new StreamReader(response.GetResponseStream());

                //read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
                if (read.BaseStream != null )
                {
                    successCallback(read);

                    read.Close();
                }
            }, request);

        }

        /// <summary>
        /// POST同步调用WCF服务    通用，不管存不存在中文问题
        /// read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void SendPostSync(string url, string bodyContent, Action<StreamReader> successCallback)
        {
            byte[] body_bytes = Encoding.UTF8.GetBytes(bodyContent);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = body_bytes.Length;
            request.ContentType = "application/json";

            //StreamWriter write = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            Stream write = request.GetRequestStream();
            write.Write(body_bytes, 0, body_bytes.Length);
            write.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (StreamReader read = new StreamReader(response.GetResponseStream()))
            {
                //read.BaseStream()不可能等于null，并且length不可能小于0  因为POST的就算是null的那么返回方式还是{"方法名Result":null}
                successCallback(read);
            }
        }



        /// <summary>
        /// GET同步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static void SendGetSync(string url, Action<StreamReader> successCallback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            //同步调用
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader read = new StreamReader(response.GetResponseStream());


            if (read.BaseStream != null)
            {
                successCallback(read);

                read.Close();
            }
        }

        /// <summary>
        /// GET异步调用WCF服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url"></param>
        /// <param name="MakeGreeting"></param>
        public static void SendGetAsync(string url, Action<StreamReader> successCallback)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            //异步调用
            request.BeginGetResponse(delegate (IAsyncResult ar)
            {
                request = (HttpWebRequest)ar.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                StreamReader read = new StreamReader(response.GetResponseStream());

                if (read.BaseStream != null)
                {
                    successCallback(read);

                    read.Close();
                }
            }, request);
        }
    }
}
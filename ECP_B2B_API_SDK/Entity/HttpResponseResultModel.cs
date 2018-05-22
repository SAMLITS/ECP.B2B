using ECP_B2B_API_SDK.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECP_B2B_API_SDK.Entity
{
    /// <summary>
    /// HTTP 请求 返回对象
    /// </summary>
    public class HttpResponseResultModel
    {
        public HttpResponseResultModel()
        { }
        public HttpResponseResultModel(DoResult Result = DoResult.Success)
        {
            this.Result = Result;
        }

        public HttpResponseResultModel(DoResult Result = DoResult.Success, string PromptMsg = null)
        {
            this.Result = Result;
            this.PromptMsg = PromptMsg;
        }
        public HttpResponseResultModel(DoResult Result = DoResult.Success, int NumberMsg = 0)
        {
            this.Result = Result;
            this.NumberMsg = NumberMsg;
        }
        /// <summary>
        /// 调试信息
        /// </summary> 
        public string DebugMessage { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary> 
        public string PromptMsg { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary> 
        public DoResult Result { get; set; }
        /// <summary>
        /// 消息编码
        /// </summary> 
        public int NumberMsg { get; set; }
        /// <summary>
        /// 携带数据Id数据
        /// </summary> 
        public int Id { get; set; }
        /// <summary>
        /// 存放返回数据
        /// </summary>
        public dynamic Data { get; set; }

        public T GetData<T>()
        {
            return ExtendMethod.DynamicToObject<T>(Data);
        }
    }

    public enum DoResult
    {
        /// <summary>
        /// 错误异常
        /// </summary> 
        Failed = 0,
        /// <summary>
        /// 成功
        /// </summary> 
        Success = 1,
        /// <summary>
        /// 超时
        /// </summary> 
        OverTime = 2,
        /// <summary>
        /// 其它
        /// </summary> 
        Other = 255,
        /// <summary>
        /// 校验错误
        /// </summary> 
        ValidError = 3,
        /// <summary>
        /// 无访问权限
        /// </summary> 
        NotAuthority = 4
    }
}

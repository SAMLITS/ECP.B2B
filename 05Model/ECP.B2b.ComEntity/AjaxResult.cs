using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity
{
    [ProtoContract]
    public class AjaxResult<R> where R : class
    {
        public AjaxResult()
        { }
        public AjaxResult(R RetValue = null, DoResult Result = DoResult.Success, string PromptMsg = null)
        {
            this.Result = Result;
            this.PromptMsg = PromptMsg;
            this.RetValue = RetValue;
        }

        public AjaxResult(R RetValue, string PromptMsg, DoResult Result = DoResult.Success)
        {
            this.Result = Result;
            this.PromptMsg = PromptMsg;
            this.RetValue = RetValue;
        }

        public AjaxResult(string PromptMsg, DoResult Result = DoResult.Success)
        {
            this.PromptMsg = PromptMsg;
            this.Result = Result;
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        [ProtoMember(1)]
        public string DebugMessage { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        [ProtoMember(2)]
        public string PromptMsg { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary>
        [ProtoMember(3)]
        public DoResult Result { get; set; }
        /// <summary>
        /// 放数据
        /// </summary>
        [ProtoMember(4)]
        public R RetValue { get; set; }
        /// <summary>
        /// RetValue之外的数据
        /// </summary>
        [ProtoMember(5)]
        public string Tag { get; set; }

        /// <summary>
        /// 携带数据Id数据
        /// </summary>
        [ProtoMember(6)]
        public int Id { get; set; }
    }

    [ProtoContract]
    public class AjaxResult 
    {
        public AjaxResult()
        { }
        public AjaxResult( DoResult Result = DoResult.Success, string PromptMsg = null)
        {
            this.Result = Result;
            this.PromptMsg = PromptMsg; 
        }
        public AjaxResult(DoResult Result = DoResult.Success, int NumberMsg = 0)
        {
            this.Result = Result;
            this.NumberMsg = NumberMsg;
        }

        public AjaxResult( string PromptMsg, DoResult Result = DoResult.Success)
        {
            this.Result = Result;
            this.PromptMsg = PromptMsg; 
        } 

        /// <summary>
        /// 调试信息
        /// </summary>
        [ProtoMember(1)]
        public string DebugMessage { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        [ProtoMember(2)]
        public string PromptMsg { get; set; }

       

        /// <summary>
        /// 操作结果
        /// </summary>
        [ProtoMember(3)]
        public DoResult Result { get; set; }

        /// <summary>
        /// 消息编码
        /// </summary>
        [ProtoMember(4)]
        public int NumberMsg { get; set; }
        
        /// <summary>
        /// 携带数据Id数据
        /// </summary>
        [ProtoMember(5)]
        public int Id { get; set; }

        /// <summary>
        /// 存放返回数据
        /// </summary>
        public string Data { get; set; } 

    }

    [ProtoContract]
    public enum DoResult
    {
        /// <summary>
        /// 错误异常
        /// </summary>
        [ProtoMember(1)]
        Failed = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [ProtoMember(2)]
        Success = 1,
        /// <summary>
        /// 超时
        /// </summary>
        [ProtoMember(3)]
        OverTime = 2,
        /// <summary>
        /// 其它
        /// </summary>
        [ProtoMember(4)]
        Other = 255,
        /// <summary>
        /// 校验错误
        /// </summary>
        [ProtoMember(5)]
        ValidError = 3,

        /// <summary>
        /// 无访问权限
        /// </summary>
        [ProtoMember(6)]
        NotAuthority =4
    }
}

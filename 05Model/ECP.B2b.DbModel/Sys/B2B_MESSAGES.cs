using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 消息
    /// </summary>
    [Serializable]
    [ProtoContract]
    [IdentityGroupUnique("3023,MESSAGE_NUMBER[NULL]|3024,MESSAGE_NAME")]
    
    public class B2B_MESSAGES: BaseModel
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        [ProtoMember(13)]
        public string MESSAGE_NUMBER { get; set; }

        /// <summary>
        /// 消息代码
        /// </summary>
        [ProtoMember(14)]
        public string MESSAGE_NAME { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(15)]
        public string MESSAGE_TEXT { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(16)]
        public string MESSAGE_TYPE { get; set; }

        /// <summary>
        /// 说明  
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(17)]
        public string MESSAGE_DESCRIPTION { get; set; }
    }
}

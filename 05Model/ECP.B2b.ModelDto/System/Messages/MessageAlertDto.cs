using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.Messages
{
    [ProtoContract]
    public class MessageAlertDto
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        [ProtoMember(1)]
        public string MESSAGE_NUMBER { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [ProtoMember(2)]
        public string MESSAGE_TEXT { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [ProtoMember(3)]
        public string MESSAGE_TYPE { get; set; }
    }
}

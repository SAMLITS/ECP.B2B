using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.Messages
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        [ProtoMember(2)]
        public string MESSAGE_NUMBER { get; set; }

        /// <summary>
        /// 消息代码
        /// </summary>
        [ProtoMember(3)]
        public string MESSAGE_NAME { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [ProtoMember(4)]
        public string MESSAGE_TEXT { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [ProtoMember(5)]
        public string MESSAGE_TYPE { get; set; }
        public string MESSAGE_TYPE_NAME { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [ProtoMember(6)]
        public string MESSAGE_DESCRIPTION { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(7)]
        public DateTime? CREATION_DATE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ProtoMember(8)]
        public string CREATOR { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [ProtoMember(9)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ProtoMember(10)]
        public string EDITOR { get; set; }
    }
}

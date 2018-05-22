using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.LookUpValuesAll
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        /// <summary>
        /// 码表Id
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }

        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(2)]
        public string LOOKUP_TYPE { get; set; }

        /// <summary>
        /// 码表名称
        /// </summary>
        [ProtoMember(3)]
        public string LOOKUP_TYPE_NAME { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [ProtoMember(4)]
        public string LOOKUP_DESCRIPTION { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(5)]
        public DateTime? CREATION_DATE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ProtoMember(6)]
        public string CREATOR { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [ProtoMember(7)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ProtoMember(8)]
        public string EDITOR { get; set; }
    }
}

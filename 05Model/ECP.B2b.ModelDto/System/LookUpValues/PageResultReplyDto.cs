using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.LookUpValues
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
        /// 码表Id
        /// </summary>
        [ProtoMember(2)]
        public int? LOOKUP_VALUES_ALL_ID { get; set; }

        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(3)]
        public string LOOKUP_TYPE { get; set; }

        /// <summary>
        /// 值代码
        /// </summary>
        [ProtoMember(4)]
        public string LOOKUP_CODE { get; set; }

        /// <summary>
        /// 值名称
        /// </summary>
        [ProtoMember(5)]
        public string LOOKUP_MEANING { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [ProtoMember(6)]
        public string LOOKUP_DESCRIPTION { get; set; }

        /// <summary>
        /// 启用标识     
        /// </summary>
        [ProtoMember(7)]
        public string ENABLED_FLAG { get; set; }
        public string ENABLED_FLAG_NAME { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [ProtoMember(8)]
        public string TAG { get; set; }

        /// <summary>
        /// 启用日期     
        /// </summary>
        [ProtoMember(9)]
        public DateTime? START_DATE_ACTIVE { get; set; }

        /// <summary>
        ///  失效日期    
        /// </summary>
        [ProtoMember(10)]
        public DateTime? END_DATE_ACTIVE { get; set; }

        /// <summary>
        ///  属性1    
        /// </summary>
        [ProtoMember(11)]
        public string ATTIBUTE1 { get; set; }

        /// <summary>
        ///  属性2    
        /// </summary>
        [ProtoMember(12)]
        public string ATTIBUTE2 { get; set; }

        /// <summary>
        /// 属性3     
        /// </summary>
        [ProtoMember(13)]
        public string ATTIBUTE3 { get; set; }

        /// <summary>
        /// 属性4     
        /// </summary>
        [ProtoMember(14)]
        public string ATTIBUTE4 { get; set; }

        /// <summary>
        /// 属性5     
        /// </summary>
        [ProtoMember(15)]
        public string ATTIBUTE5 { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(16)]
        public DateTime? CREATION_DATE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ProtoMember(17)]
        public string CREATOR { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [ProtoMember(18)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ProtoMember(19)]
        public string EDITOR { get; set; }
    }
}

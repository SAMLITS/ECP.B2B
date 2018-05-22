using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 码表值
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class B2B_LOOKUP_VALUES : BaseModel
    {

        /// <summary>
        /// 码表ID
        /// </summary>
        [ProtoMember(13)]
        public int? LOOKUP_VALUES_ALL_ID { get; set; }

        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(14)]
        public string LOOKUP_TYPE { get; set; }

        /// <summary>
        /// 值代码
        /// </summary>
        [ProtoMember(15)]
        public string LOOKUP_CODE { get; set; }

        /// <summary>
        /// 值名称
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(16)]
        public string LOOKUP_MEANING { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(17)]
        public string LOOKUP_DESCRIPTION { get; set; }

        /// <summary>
        /// 启用标识     
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(18)]
        public string ENABLED_FLAG { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(19)]
        public string TAG { get; set; }

        /// <summary>
        /// 启用日期     
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(20)]
        public DateTime? START_DATE_ACTIVE { get; set; }

        /// <summary>
        ///  失效日期    
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(21)]
        public DateTime? END_DATE_ACTIVE { get; set; }

        /// <summary>
        ///  属性1    
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(22)]
        public string ATTIBUTE1 { get; set; }

        /// <summary>
        ///  属性2    
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(23)]
        public string ATTIBUTE2 { get; set; }

        /// <summary>
        /// 属性3     
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(24)]
        public string ATTIBUTE3 { get; set; }

        /// <summary>
        /// 属性4     
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(25)]
        public string ATTIBUTE4 { get; set; }

        /// <summary>
        /// 属性5     
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(26)]
        public string ATTIBUTE5 { get; set; }
    }
}

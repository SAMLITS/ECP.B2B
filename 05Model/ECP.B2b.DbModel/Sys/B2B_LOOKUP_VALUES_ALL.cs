using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 码表
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class B2B_LOOKUP_VALUES_ALL: BaseModel
    {
        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(13)]
        public string LOOKUP_TYPE { get; set; }

        /// <summary>
        /// 码表名称
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(14)]
        public string LOOKUP_TYPE_NAME { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(15)]
        public string LOOKUP_DESCRIPTION { get; set; }
    }
}

using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 菜单功能
    /// </summary>
    [Serializable]
    [ProtoContract]
    [IdentityGroupUnique("3234,FUNCTION_CODE[NULL]")]
    public class B2B_MENU_FUNCTION : BaseModel
    { 
        /// <summary>
        /// 菜单ID
        /// </summary>
        [ProtoMember(13)]
        public int? MENU_ID { get; set; }

        /// <summary>
        /// 功能代码
        /// </summary>
        [ProtoMember(14)]
        public string FUNCTION_CODE { get; set; }

        /// <summary>
        /// 功能说明
        /// </summary>
        [ProtoMember(15)]
        public string FUNCTION_DESC { get; set; }

        /// <summary>
        /// 是否默认分配
        /// </summary>
        [ProtoMember(23)]
        public string DEFAULT_ASSIGN { get; set; } = "N";

        /// <summary>
        /// 备注
        /// </summary>
        [ProtoMember(24)]
        public string REMARK { get; set; }
    }
}

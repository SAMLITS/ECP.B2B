using ECP.B2b.AttributeModel;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.DbModel.Basic;
using ProtoBuf;
using System;
using System.ComponentModel.DataAnnotations; 

namespace ECP.B2b.DbModel
{
    /// <summary>
    /// [ProtoMember(序号)]  保留到12，子类中 序号从13开始
    /// </summary>
    [Serializable]
    [ProtoContract]
    [ProtoInclude(13, typeof(B2B_MENU))]
    [ProtoInclude(14, typeof(B2B_LOOKUP_VALUES_ALL))]
    [ProtoInclude(15, typeof(B2B_LOOKUP_VALUES))]
    [ProtoInclude(16, typeof(B2B_MESSAGES))] 
    [ProtoInclude(18, typeof(B2B_USER))]
    [ProtoInclude(19, typeof(B2B_USER_MENU))]  
    [ProtoInclude(82, typeof(B2B_MENU_FUNCTION))]
    [ProtoInclude(83, typeof(B2B_USER_FUNCTION))] 
    public class BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [ProtoMember(1)]
        public int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ProtoMember(2)]
        public DateTime? CREATION_DATE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ProtoMember(3)]
        public string CREATOR { get; set; }

        /// <summary>
        /// 最近修改时间
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(4)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(5)]
        public string EDITOR { get; set; }

        /// <summary>
        /// 删除标识 0-未删除；1-已删除
        /// </summary>
        [ProtoMember(6)]
        public int? DEL_FLAG { get; set; } = 0;
    }
}

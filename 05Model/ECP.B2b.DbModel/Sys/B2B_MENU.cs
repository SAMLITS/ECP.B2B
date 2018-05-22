using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Serializable]
    [ProtoContract]
    [IdentityGroupUnique("2012,ORDER,MENU_TYPE,MAIN_MENU_ID|3039,MENU_CODE[NULL]")]
    [AvailRemark(AvaliType.Equals, "IS_AVAILABLE", "Y")]
    public class B2B_MENU : BaseModel
    { 
        /// <summary>
        /// 菜单类型 
        /// </summary>
        [ProtoMember(13)]
        public string MENU_TYPE { get; set; }

        /// <summary>
        /// 主菜单ID
        /// </summary>
        [ProtoMember(14)]
        public int? MAIN_MENU_ID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(15)]
        public string MENU_NAME { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(16)]
        public int? ORDER { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(17)]
        public string MENU_URL { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(18)]
        public string IMAGEURL { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(19)]
        public string REMARK { get; set; }

        /// <summary>
        /// 菜单种类
        /// </summary>
        [ProtoMember(20)]
        public string MENU_SORT { get; set; }

        /// <summary>
        /// 适用终端类别
        /// </summary>
        [ProtoMember(21)]
        public string TERMINAL_TYPE { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        [ProtoMember(22)]
        public string MENU_CODE { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(23)]
        public string IS_AVAILABLE { get; set; } = "Y";

        [ProtoMember(24)]
        public string MENU_PATH { get; set; }

        /// <summary>
        /// 是否可分配
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(25)]
        public string IS_ALLOCATED { get; set; } = "Y";
        
    }
}

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Basic
{

    /// <summary>
    /// 用户功能分配
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class B2B_USER_FUNCTION : BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [ProtoMember(13)]
        public int? USER_ID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [ProtoMember(14)]
        public int? MENU_ID { get; set; }

        /// <summary>
        /// 菜单功能ID
        /// </summary>
        [ProtoMember(15)]
        public int? MENU_FUNCTION_ID { get; set; }


        /// <summary>
        /// 功能代码
        /// </summary>
        [ProtoMember(16)]
        public string MENU_FUNCTION_CODE { get; set; }
    }
}

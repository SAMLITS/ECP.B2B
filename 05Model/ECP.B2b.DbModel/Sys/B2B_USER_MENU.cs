using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 用户菜单分配
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class B2B_USER_MENU : BaseModel
    {
        [ProtoMember(1)]
        public int? USER_ID { get; set; }
        [ProtoMember(2)]
        public int? MAIN_MENU_ID { get; set; }
        [ProtoMember(3)]
        public int? SUBMENU_ID { get; set; }
        [ProtoMember(4)]
        public string REMARK { get; set; }

    }
}

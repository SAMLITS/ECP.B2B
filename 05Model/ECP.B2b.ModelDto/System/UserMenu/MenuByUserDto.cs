using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.UserMenu
{
    /// <summary>
    /// 根据用户返回对应的菜单id数据
    /// </summary>
    [ProtoContract]
    public class MenuByUserDto
    {
        [ProtoMember(1)]
        public int? MAIN_MENU_ID { get; set; }
        [ProtoMember(2)]
        public int? SUBMENU_ID { get; set; }
        [ProtoMember(3)]
        public int ID { get; set; }
    }
}

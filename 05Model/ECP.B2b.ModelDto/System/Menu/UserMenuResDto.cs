using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.Menu
{
    /// <summary>
    /// 响应用户查找菜单结果
    /// </summary>
    [ProtoContract]
    public class UserMenuResDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        /// <summary>
        ///  菜单类型 M-主菜单；S-子菜单
        /// </summary>
        [ProtoMember(2)]
        public string MENU_TYPE { get; set; }
        /// <summary>
        /// 主菜单ID
        /// </summary>
        [ProtoMember(3)]
        public int? MAIN_MENU_ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [ProtoMember(4)]
        public string MENU_NAME { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [ProtoMember(5)]
        public int? ORDER { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        [ProtoMember(6)]
        public string MENU_URL { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [ProtoMember(7)]
        public string IMAGEURL { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        [ProtoMember(8)]
        public string MENU_PATH { get; set; }
    }

    /// <summary>
    /// 响应用户查找菜单结果
    /// </summary>
    [ProtoContract]
    public class UserMenuDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        /// <summary>
        ///  菜单类型 M-主菜单；S-子菜单
        /// </summary>
        [ProtoMember(2)]
        public string MENU_TYPE { get; set; }
        /// <summary>
        /// 主菜单ID
        /// </summary>
        [ProtoMember(3)]
        public int? MAIN_MENU_ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [ProtoMember(4)]
        public string MENU_NAME { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [ProtoMember(5)]
        public int? ORDER { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        [ProtoMember(6)]
        public string MENU_URL { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        [ProtoMember(7)]
        public string MENU_CODE { get; set; }

    }
    public class UserMenuResGroupDto
    {
        public UserMenuResDto mainUserMenu { get; set; }
        public List<UserMenuResDto> subUserMenus{ get; set; }
    }

}

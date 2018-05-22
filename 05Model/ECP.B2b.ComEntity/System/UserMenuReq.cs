using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.System
{
    /// <summary>
    /// 查找用户菜单 请求条件
    /// </summary>
    [ProtoContract]
    public class UserMenuReq
    {
      

        /// <summary>
        /// 菜单种类   必须
        /// </summary>
        [ProtoMember(1)]
        public string MENU_SORT { get; set; }
        /// <summary>
        /// 适用终端类别  必须
        /// </summary>
        [ProtoMember(2)]
        public string TERMINAL_TYPE { get; set; }

        /// <summary>
        /// ID集合  为null 则忽略此条件查询所有
        /// </summary>
        [ProtoMember(3)]
        public List<int> IDs { get; set; }
         
    }
}

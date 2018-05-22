using ECP.B2b.AttributeModel;
using ECP.B2b.DbModel.Sys;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbExtendModel.System
{
    [Serializable]
    [ProtoContract]
    public class B2B_MENU_Extend : B2B_MENU
    {
        /// <summary>
        /// 适用终端类别-TERMINAL_TYPE：PC-PC端；APP-微信端
        /// </summary>
        [ProtoMember(1)]
        public string TERMINAL_TYPE_NAME { get; set; }


        /// <summary>
        /// 父层菜单名
        /// </summary>
        [ProtoMember(2)]
        public string MAIN_MENU_NAME { get; set; }
    }
}

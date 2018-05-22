using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.Menu
{
    public class AppUserMenuResDto
    { 
        public int ID { get; set; }
        /// <summary>
        ///  菜单类型 M-主菜单；S-子菜单
        /// </summary> 
        public string MENU_TYPE { get; set; }
        /// <summary>
        /// 主菜单ID
        /// </summary> 
        public int? MAIN_MENU_ID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary> 
        public string MENU_NAME { get; set; }
        /// <summary>
        /// 序号
        /// </summary> 
        public int? ORDER { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary> 
        public string MENU_URL { get; set; }

        /// <summary>
        /// 图标
        /// </summary> 
        public string IMAGEURL { get; set; }
    }
}

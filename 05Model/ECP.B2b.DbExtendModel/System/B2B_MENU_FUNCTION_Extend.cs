using ECP.B2b.DbModel.Sys;
using ProtoBuf;

namespace ECP.B2b.DbExtendModel.System
{
    public class B2B_MENU_FUNCTION_Extend : B2B_MENU_FUNCTION
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [ProtoMember(1)]
        public string MENU_NAME { get; set; }
    }
}

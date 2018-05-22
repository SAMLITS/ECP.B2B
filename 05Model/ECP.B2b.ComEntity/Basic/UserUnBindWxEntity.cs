using ECP.B2b.DbModel.Basic;
using ECP.B2b.DbModel.Sys;
using ProtoBuf;


namespace ECP.B2b.ComEntity.Basic
{
    /// <summary>
    /// 用户微信解除绑定提交实体
    /// </summary>
    [ProtoContract]
    public class UserUnBindWxEntity
    {
        /// <summary>
        /// 用户实体
        /// </summary>
        [ProtoMember(1)]
        public B2B_USER b2b_USER { get; set; } 
    }
}

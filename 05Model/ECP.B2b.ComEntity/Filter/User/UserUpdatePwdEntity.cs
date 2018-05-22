using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.User
{
    /// <summary>
    /// 修改密码实体
    /// </summary>
    [ProtoContract]
    public class UserUpdatePwdEntity
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string OldPwd { get; set; }
        [ProtoMember(3)]
        public string NewPwd { get; set; }
    }
}

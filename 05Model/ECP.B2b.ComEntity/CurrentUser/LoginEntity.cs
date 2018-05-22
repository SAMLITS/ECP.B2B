using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.CurrentUser
{
    [ProtoContract]
    public class LoginEntity
    {
        [ProtoMember(1)]
        public string USER { get; set; }
        [ProtoMember(2)]
        public string PASSWORD { get; set; }
    }
}

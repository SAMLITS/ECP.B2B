using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity
{
    [ProtoContract]
    public class IdModel
    { 
        [ProtoMember(1)]
        public int ID { get; set; }
    }
}

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity
{
    [ProtoContract]
    public class RemoveModel
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public DateTime LAST_UPDATE_DATE { get; set; }
        [ProtoMember(3)]
        public string EDITOR { get; set; }
    }
}

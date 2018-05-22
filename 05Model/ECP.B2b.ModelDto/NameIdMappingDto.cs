using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto
{
    /// <summary>
    /// 根据Id查询对应名称返回DTO / 根据名称查询出对应的Id
    /// </summary>
    [ProtoContract]
    public class NameByIdDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public string NAME1 { get; set; }
        [ProtoMember(3)]
        public string NAME2 { get; set; }
        [ProtoMember(4)]
        public string NAME3 { get; set; }
        [ProtoMember(5)]
        public string NAME4 { get; set; }
        [ProtoMember(6)]
        public string NAME5 { get; set; }
        [ProtoMember(7)]
        public string NAME6 { get; set; }
        [ProtoMember(8)]
        public string NAME7 { get; set; }
    }
     
}

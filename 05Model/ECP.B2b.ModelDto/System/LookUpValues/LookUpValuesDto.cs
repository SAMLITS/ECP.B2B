using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.LookUpValues
{
    [ProtoContract]
    public class LookUpValuesByTypeDto
    {
        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(1)]
        public string LOOKUP_TYPE { get; set; }

        /// <summary>
        /// 值代码
        /// </summary>
        [ProtoMember(2)]
        public string LOOKUP_CODE { get; set; }

        /// <summary>
        /// 值名称
        /// </summary>
        [ProtoMember(3)]
        public string LOOKUP_MEANING { get; set; }

        [ProtoMember(4)]
        public string ATTIBUTE1 { get; set; }
        [ProtoMember(5)]
        public string ATTIBUTE2 { get; set; }
        [ProtoMember(6)]
        public string ATTIBUTE3 { get; set; }
        [ProtoMember(7)]
        public string ATTIBUTE4 { get; set; }
        [ProtoMember(8)]
        public string ATTIBUTE5 { get; set; }
        [ProtoMember(9)]
        public string TAG { get; set; }
        [ProtoMember(10)]
        public string LOOKUP_DESCRIPTION { get; set; }
    }
}

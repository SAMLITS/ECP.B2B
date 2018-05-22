using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.LookUpValues
{
    [ProtoContract]
    public class LookUpValuesByTypeParams
    {
        /// <summary>
        /// 码表代码
        /// </summary>
        [ProtoMember(1)]
        public string LookUpName { get; set; }

        /// <summary>
        /// 是否只取有效数据
        /// </summary>
        [ProtoMember(2)]
        public bool IsBetweenOt { get; set; } = false;

        /// <summary>
        /// 指定Code数据
        /// </summary>
        [ProtoMember(3)]
        public List<string> LOOKUP_CODE_List { get; set; }

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


        /// <summary>
        /// 默认值   不会涉及到查询业务操作，只是临时保存默认值
        /// </summary>
        public string defaultValue { get; set; }

    }
}

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity
{
    /// <summary>
    /// 根据 ID 查询 对应名称 参数
    /// </summary>
    [ProtoContract]
    public class NameByIdParams
    {
        [ProtoMember(1)]
        public List<int> IdList { get; set; }
        [ProtoMember(2)]
        public string MappingDbField1 { get; set; }
        [ProtoMember(3)]
        public string MappingDbField2 { get; set; }
        [ProtoMember(4)]
        public string MappingDbField3 { get; set; }
        [ProtoMember(5)]
        public string MappingDbField4 { get; set; }
        [ProtoMember(6)]
        public string MappingDbField5 { get; set; }

        [ProtoMember(7)]
        public string MappingDbField6 { get; set; }
        [ProtoMember(8)]
        public string MappingDbField7 { get; set; }
    }

    /// <summary>
    /// 根据名称模糊 查询 对应 ID集合   参数
    /// </summary>
    [ProtoContract]
    public class IdByNameContainsParams
    {
        [ProtoContract]
        public class QueryWhereName
        {
            /// <summary>
            /// 需要查询的值
            /// </summary>
            [ProtoMember(1)]
            public string NameVal { get; set; }
            /// <summary>
            /// 是否模糊查询
            /// </summary>
            [ProtoMember(2)]
            public bool IsLike { get; set; }



            [ProtoMember(3)]
            public List<int> NameValIntList { get; set; }
        }

        [ProtoMember(1)]
        public List<QueryWhereName> queryWheres { get; set; }

        /// <summary>
        /// 查询方式DB名称   将通过该属性指定的DB字段名进行名称条件查询
        /// </summary>
        [ProtoMember(3)]
        public string MappingDbField1 { get; set; }
        [ProtoMember(4)]
        public string MappingDbField2 { get; set; }
        [ProtoMember(5)]
        public string MappingDbField3 { get; set; }
        [ProtoMember(6)]
        public string MappingDbField4 { get; set; }
        [ProtoMember(7)]
        public string MappingDbField5 { get; set; }

        [ProtoMember(8)]
        public string MappingDbField6 { get; set; }
        [ProtoMember(9)]
        public string MappingDbField7 { get; set; }
    }
}

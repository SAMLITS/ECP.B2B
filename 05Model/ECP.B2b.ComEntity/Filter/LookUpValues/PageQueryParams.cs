using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.LookUpValues
{
    [ProtoContract]
    public class PageQueryParams
    {
        /// <summary>
        /// 查询参数类
        /// </summary>
        [ProtoContract]
        public class QueryParamsRequest
        {
            /// <summary>
            /// 码表代码
            /// </summary>
            [ProtoMember(1)]
            [QueryParamsBuildOptions(ExpressionOptions.Equals)]
            public int? LOOKUP_VALUES_ALL_ID { get; set; }
        }

        /// <summary>
        /// 页面参数信息
        /// </summary>
        [ProtoMember(1)]
        public PageFilter PageInfo { get; set; }

        /// <summary>
        /// 查询参数信息
        /// </summary>
        [ProtoMember(2)]
        public QueryParamsRequest QueryParams { get; set; } = new QueryParamsRequest();

    }
}

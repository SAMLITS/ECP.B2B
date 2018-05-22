using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.LookUpValuesAll
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
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string LOOKUP_TYPE { get; set; }

            /// <summary>
            /// 码表代码 模糊查询
            /// </summary>
            [ProtoMember(2)]
            public bool IS_LIKE_LOOKUP_TYPE { get; set; }


            /// <summary>
            /// 码表名称
            /// </summary>
            [ProtoMember(3)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string LOOKUP_TYPE_NAME { get; set; }

            /// <summary>
            /// 码表名称 模糊查询
            /// </summary>
            [ProtoMember(4)]
            public bool IS_LIKE_LOOKUP_TYPE_NAME { get; set; }
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

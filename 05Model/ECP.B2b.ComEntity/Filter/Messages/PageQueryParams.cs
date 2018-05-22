using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.Messages
{
    [ProtoContract]
    public class PageQueryParams
    {
        // <summary>
        /// 消息查询参数类
        /// </summary>
        [ProtoContract]
        public class QueryParamsRequest
        {
            /// <summary>
            /// 消息编号
            /// </summary>
            [ProtoMember(1)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string MESSAGE_NUMBER { get; set; }

            /// <summary>
            /// 消息编号 模糊查询
            /// </summary>
            [ProtoMember(2)]
            public bool IS_LIKE_MESSAGE_NUMBER { get; set; }

            /// <summary>
            /// 消息代码
            /// </summary>
            [ProtoMember(3)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string MESSAGE_NAME { get; set; }

            /// <summary>
            /// 消息代码 模糊查询
            /// </summary>
            [ProtoMember(4)]
            public bool IS_LIKE_MESSAGE_NAME { get; set; }

            /// <summary>
            /// 消息类型
            /// </summary>
            [ProtoMember(5)]
            [QueryParamsBuildOptions(ExpressionOptions.Equals)]
            public string MESSAGE_TYPE { get; set; }

            /// <summary>
            /// 消息内容
            /// </summary>
            [ProtoMember(6)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string MESSAGE_TEXT { get; set; }

            /// <summary>
            /// 消息内容 模糊查询
            /// </summary>
            [ProtoMember(7)]
            public bool IS_LIKE_MESSAGE_TEXT { get; set; }

            [ProtoMember(8)]
            [PageQueryFieldMapping("CREATION_DATE")]
            [QueryParamsBuildOptions(ExpressionOptions.GreaterThanOrEqual)]
            public DateTime? START_DATE { get; set; }
            [ProtoMember(9)]
            [PageQueryFieldMapping("CREATION_DATE")]
            [QueryParamsBuildOptions(ExpressionOptions.LessThanOrEqual)]
            public DateTime? END_DATE { get; set; }
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
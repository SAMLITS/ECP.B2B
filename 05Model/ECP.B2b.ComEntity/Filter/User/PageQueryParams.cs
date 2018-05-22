using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace ECP.B2b.ComEntity.Filter.User
{
    [ProtoContract]
    public class PageQueryParams
    {
        [ProtoContract]
        public class QueryParamsRequest
        {
            /// <summary>
            /// 交易方名称（外键）
            /// </summary>
            [ProtoMember(1)]
            [NotQueryParamsBuildOptions]
            public string PARTY_NAME { get; set; }

            /// <summary>
            ///交易方名称（外键） 模糊查询
            /// </summary>
            [ProtoMember(2)]
            public bool IS_LIKE_PARTY_NAME { get; set; }

            /// <summary>
            /// 交易方ID集合
            /// </summary>
            [ProtoMember(3)]
            [QueryParamsBuildOptions(ExpressionOptions.ContainsList)]
            public List<int> PARTY_ID { get; set; }

            /// <summary>
            /// 交易方类型
            /// </summary>
            [ProtoMember(4)]
            [QueryParamsBuildOptions(ExpressionOptions.Equals)]
            public string PARTY_TYPE { get; set; }

            /// <summary>
            /// 注册状态
            /// </summary>
            [ProtoMember(5)]
            [QueryParamsBuildOptions(ExpressionOptions.Equals)]
            public string REG_STATUS { get; set; }

            /// <summary>
            /// 真实姓名
            /// </summary>
            [ProtoMember(6)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string USER_NAME { get; set; }

            /// <summary>
            /// 真实姓名 模糊查询
            /// </summary>
            [ProtoMember(7)]
            public bool IS_LIKE_USER_NAME { get; set; }

            /// <summary>
            /// 用户账户
            /// </summary>
            [ProtoMember(8)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string USER { get; set; }

            /// <summary>
            ///  用户账户 模糊查询
            /// </summary>
            [ProtoMember(9)]
            public bool IS_LIKE_USER { get; set; }

            [ProtoMember(10)]
            [PageQueryFieldMapping("CREATION_DATE")]
            [QueryParamsBuildOptions(ExpressionOptions.GreaterThanOrEqual)]
            public DateTime? START_DATE { get; set; }
            [ProtoMember(11)]
            [PageQueryFieldMapping("CREATION_DATE")]
            [QueryParamsBuildOptions(ExpressionOptions.LessThanOrEqual)]
            public DateTime? END_DATE { get; set; }

            /// <summary>
            /// 是否主帐号
            /// </summary>
            [ProtoMember(12)]
            public string IS_MAIN { get; set; }
        }

        [ProtoMember(1)]
        public PageFilter PageInfo { get; set; }

        [ProtoMember(2)]
        public QueryParamsRequest QueryParams { get; set; } = new QueryParamsRequest();

    }
}

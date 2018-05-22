using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.Menu
{
 
     [ProtoContract]
    public class CQueryPageQueryParams
    {
        /// <summary>
        /// 菜单控件查询参数类
        /// </summary>
        [ProtoContract]
        public class QueryParamsRequest
        {
            [ProtoMember(1)]
            public string MENU_TYPE { get; set; }
            [ProtoMember(2)]
            public bool IS_LIKE_MENU_NAME { get; set; }
            [ProtoMember(3)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string MENU_NAME { get; set; }
            [ProtoMember(4)]
            public string MENU_SORT { get; set; }
            [ProtoMember(5)]
            public string TERMINAL_TYPE { get; set; }

            [ProtoMember(6)]
            public int? MAIN_MENU_ID { get; set; }

            [ProtoMember(7)]
            public bool IS_LIKE_MENU_CODE { get; set; }
            [ProtoMember(8)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string MENU_CODE { get; set; }

            [ProtoMember(9)]
            public string IS_AVAILABLE { get; set; }

            /// <summary>
            /// 是否根据有效设置 只是读取有效数据
            /// </summary>
            [ProtoMember(10)]
            [NotQueryParamsBuildOptions]
            public bool IS_QUERY_AVAIL { get; set; }
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

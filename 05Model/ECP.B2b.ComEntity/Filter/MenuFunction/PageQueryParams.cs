using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.MenuFunction
{
    [ProtoContract]
    public class PageQueryParams
    {
        [ProtoContract]
        public class QueryParamsRequest 
        {
            [ProtoMember(1)]
            public int? MENU_ID { get; set; }

            [ProtoMember(2)]
            public bool IS_LIKE_FUNCTION_CODE { get; set; }
            [ProtoMember(3)]
            [QueryParamsBuildOptions(ExpressionOptions.Normal)]
            public string FUNCTION_CODE { get; set; }

            [ProtoMember(4)]
            public string DEFAULT_ASSIGN { get; set; }
        }


        [ProtoMember(1)]
        public PageFilter PageInfo { get; set; }
        [ProtoMember(2)]
        public QueryParamsRequest QueryParams { get; set; } = new QueryParamsRequest();

    }
}

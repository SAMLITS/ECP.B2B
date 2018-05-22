using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.Menu
{
    [ProtoContract]
    public class PageQueryParams
    {
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
        }


        [ProtoMember(1)]
        public PageFilter PageInfo { get; set; }
        [ProtoMember(2)]
        public QueryParamsRequest QueryParams { get; set; } = new QueryParamsRequest();

    }
}

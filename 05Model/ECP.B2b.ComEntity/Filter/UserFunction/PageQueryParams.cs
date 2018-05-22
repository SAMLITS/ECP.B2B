using ECP.B2b.AttributeModel;
using ECP.B2b.ComEntity.Page;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.Filter.UserFunction
{
    [ProtoContract]
    public class PageQueryParams
    {
        [ProtoContract]
        public class QueryParamsRequest 
        {
            [ProtoMember(1)]
            public int? USER_ID { get; set; }

            [ProtoMember(2)]
            public int? MENU_ID { get; set; }

            [ProtoMember(3)]
            public int? MENU_FUNCTION_ID { get; set; }
        }


        [ProtoMember(1)]
        public PageFilter PageInfo { get; set; }
        [ProtoMember(2)]
        public QueryParamsRequest QueryParams { get; set; } = new QueryParamsRequest();

    }
}

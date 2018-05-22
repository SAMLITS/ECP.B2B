using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;

namespace ECP.B2b.ModelDto.System.MenuFunction
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public int? MENU_ID { get; set; }
        [ProtoMember(3)]
        [NotAutoBuilderLambdaSelect]
        public string MENU_NAME { get; set; }

        [ProtoMember(4)]
        public string FUNCTION_CODE { get; set; }

        [ProtoMember(5)]
        public string FUNCTION_DESC { get; set; }

        [ProtoMember(6)]
        public string DEFAULT_ASSIGN { get; set; }
        [ProtoMember(7)]
        [NotAutoBuilderLambdaSelect]
        public string DEFAULT_ASSIGN_NAME { get; set; }

        [ProtoMember(8)]
        public string REMARK { get; set; }

        [ProtoMember(9)]
        public DateTime? CREATION_DATE { get; set; }

        [ProtoMember(10)]
        public string CREATOR { get; set; }

        [ProtoMember(11)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [ProtoMember(12)]
        public string EDITOR { get; set; }
    } 
}

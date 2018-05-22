using ECP.B2b.AttributeModel;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.Menu
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public string MENU_TYPE { get; set; }
        [ProtoMember(3)]
        public string MENU_NAME { get; set; }
        [ProtoMember(4)]
        public int? ORDER { get; set; }

        [ProtoMember(5)]
        public string MENU_URL { get; set; }
        [ProtoMember(6)]
        public string MENU_SORT { get; set; }

        [ProtoMember(7)]
        public string TERMINAL_TYPE { get; set; }
        [ProtoMember(8)]
        public DateTime? CREATION_DATE { get; set; }

        [ProtoMember(9)]
        public string CREATOR { get; set; }

        [ProtoMember(10)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [ProtoMember(11)]
        public string EDITOR { get; set; }

        [ProtoMember(12)]
        public string MENU_CODE { get; set; }

        [ProtoMember(13)]
        public int? MAIN_MENU_ID { get; set; }

        [ProtoMember(14)]
        public string IS_AVAILABLE { get; set; }

        [ProtoMember(15)]
        public string MENU_PATH { get; set; }

        [ProtoMember(16)]
        public string IS_ALLOCATED { get; set; }

        [NotAutoBuilderLambdaSelect]
        public string MENU_TYPE_NAME { get; set; }
        [NotAutoBuilderLambdaSelect]
        public string MENU_SORT_NAME { get; set; }
        [NotAutoBuilderLambdaSelect]
        public string TERMINAL_TYPE_NAME { get; set; }
        [NotAutoBuilderLambdaSelect]
        public string IS_AVAILABLE_NAME { get; set; }
        [NotAutoBuilderLambdaSelect]
        public string MAIN_MENU_NAME { get; set; }
        [NotAutoBuilderLambdaSelect]
        public string IS_ALLOCATED_NAME { get; set; }
    } 

}

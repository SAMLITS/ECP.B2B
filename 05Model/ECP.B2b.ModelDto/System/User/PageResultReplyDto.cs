using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ModelDto.System.User
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public string USER { get; set; }
        [ProtoMember(3)]
        public string IS_MAIN { get; set; }
        public string IS_MAIN_NAME { get; set; }
        [ProtoMember(4)]
        public string USER_NAME { get; set; }

        [ProtoMember(5)]
        public string MOBILE { get; set; }

        [ProtoMember(6)]
        public string IS_WXIN_LOGIN { get; set; }
        public string IS_WXIN_LOGIN_NAME { get; set; }

        [ProtoMember(7)]
        public string REG_STATUS { get; set; }
        public string REG_STATUS_NAME { get; set; }

        [ProtoMember(8)]
        public int? DEL_FLAG { get; set; }
        [ProtoMember(9)]
        public DateTime? CREATION_DATE { get; set; }

        [ProtoMember(10)]
        public string CREATOR { get; set; }

        [ProtoMember(11)]
        public DateTime? LAST_UPDATE_DATE { get; set; }

        [ProtoMember(12)]
        public string EDITOR { get; set; }
        [ProtoMember(13)]
        public int? PARTY_ID { get; set; }
        public string PARTY_NAME { get; set; }

        [ProtoMember(14)]
        public string PARTY_TYPE { get; set; }
        public string PARTY_TYPE_NAME { get; set; }

        [ProtoMember(15)]
        public DateTime? START_DATE { get; set; }
        [ProtoMember(16)]
        public DateTime? END_DATE { get; set; }


        [ProtoMember(17)]
        public string MAIL { get; set; }

        [ProtoMember(18)]
        public DateTime? LAST_LOGIN_DATE { get; set; }
        

    }
}

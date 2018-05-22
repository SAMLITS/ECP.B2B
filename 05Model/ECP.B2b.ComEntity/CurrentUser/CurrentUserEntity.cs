using ECP.B2b.DbModel.Basic;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.CurrentUser
{
    [ProtoContract]
    public class CurrentUserEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [ProtoMember(1)]
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [ProtoMember(2)]
        public string USER { get; set; }


        [ProtoMember(3)]
        public int PARTY_ID { get; set; }

        [ProtoMember(4)]
        public string PARTY_TYPE { get; set; }

        [ProtoMember(5)]
        public string IS_MAIN { get; set; }

        [ProtoMember(6)]
        public string USER_NAME { get; set; }

        [ProtoMember(7)]
        public string MOBILE { get; set; }

        [ProtoMember(8)]
        public string MAIL { get; set; }

        [ProtoMember(9)]
        public string IS_WXIN_LOGIN { get; set; }

        [ProtoMember(10)]
        public string REG_STATUS { get; set; }

        [ProtoMember(11)]
        public DateTime? START_DATE { get; set; }

        [ProtoMember(12)]
        public DateTime? END_DATE { get; set; }

         
        public List<string> UserFunctionEnabledList { get; set; } = new List<string>();
    }
}

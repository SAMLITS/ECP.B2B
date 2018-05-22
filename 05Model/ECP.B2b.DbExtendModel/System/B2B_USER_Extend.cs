using ECP.B2b.AttributeModel;
using ECP.B2b.DbModel.Sys;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbExtendModel.System
{
    [Serializable]
    [ProtoContract]
    public class B2B_USER_Extend: B2B_USER
    {
        /// <summary>
        /// 交易方名称
        /// </summary>
        [ProtoMember(1)]
        public string PARTY_NAME { get; set; }

        /// <summary>
        /// 是否已关联微信
        /// </summary>
        [ProtoMember(2)]
        public string IS_BIND_WXIN { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        [ProtoMember(3)]
        public string WX_NICK_NAME { get; set; }


    }
}

using ECP.B2b.AttributeModel;
using ECP.B2b.DbModel.Basic;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DbModel.Sys
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    [ProtoContract]
    [IdentityGroupUnique("3025,USER")]
    public class B2B_USER : BaseModel
    {

        /// <summary>
        /// 交易方ID
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(13)]
        //[TransFieldBind(typeof(B2B_PARTY),"ID")]
        public int? PARTY_ID { get; set; }

        /// <summary>
        /// 交易方类型
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(14)]
        public string PARTY_TYPE { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [ProtoMember(15)]
        public string USER { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(16)]
        public string PASSWORD { get; set; }

        /// <summary>
        /// 是否主账号
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(17)]
        public string IS_MAIN { get; set; } = "N";

        /// <summary>
        /// 真实姓名
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(18)]
        public string USER_NAME { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(19)]
        public string MOBILE { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(20)]
        public string MAIL { get; set; }

        /// <summary>
        /// 密码问题
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(21)]
        public string PW_QUESTION { get; set; }

        /// <summary>
        /// 密码答案
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(22)]
        public string PW_ANSWER { get; set; }

        /// <summary>
        /// 已登录标识
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(23)]
        public string LOGGINED_FLAG { get; set; } = "N";

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(24)]
        public DateTime? LAST_LOGIN_DATE { get; set; }

        /// <summary>
        /// 密码最后更新时间
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(25)]
        public DateTime? PASSWORD_UPDATE_TIME { get; set; }

        /// <summary>
        /// 是否强制微信登录
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(26)]
        public string IS_WXIN_LOGIN { get; set; } = "N";

        /// <summary>
        /// 备注
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(27)]
        public string REMARK { get; set; }

        /// <summary>
        /// 注册状态
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(28)]
        public string REG_STATUS { get; set; } = "0";

        /// <summary>
        /// 生效时间
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(29)]
        public DateTime? START_DATE { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        [ModelModifyFlag]
        [ProtoMember(30)]
        public DateTime? END_DATE { get; set; }
    }
}

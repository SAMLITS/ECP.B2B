using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.ComEntity.CurrentUser
{
    /// <summary>
    /// APP端登录返回信息
    /// </summary>
    public class AppCurrentUserEntity
    { 
        public AppUserCurrentUserEntity AppUser { get; set; }
        public AppPartyCurrentUserEntity AppParty { get; set; }
        public AppWxBindCurrentUserEntity AppWxBind { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string USER { get; set; }
    }

    public class AppUserCurrentUserEntity
    {

        public int ID { get; set; }
        public string USER { get; set; } 
        public string IS_MAIN { get; set; }
        public string USER_NAME { get; set; }
    }
    public class AppPartyCurrentUserEntity
    {
        public int ID { get; set; }
        public string PARTY_NAME { get; set; }
        public string PARTY_TYPE { get; set; }
        public string PARTY_SORT { get; set; }
        public string COMPANY_TYPE { get; set; }
    }
    public class AppWxBindCurrentUserEntity
    {
        public int ID { get; set; }
        public string WX_OPENID { get; set; }
        public string WX_NICK_NAME { get; set; }
        public string WX_AVATAR_URL { get; set; }
        public string WX_UNIONID { get; set; }
    }
}

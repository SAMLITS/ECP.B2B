using System;

namespace ECP.Util.Jwt.LTS
{
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// 密码字段是否已加密
        /// </summary>
        public bool IsPasswordEncrypt { get; set; } = false;

        public string Role { get; set; }
        public int Id { get; set; }
    }
}

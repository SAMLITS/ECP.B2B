using Microsoft.IdentityModel.Tokens;

namespace ECP.Util.Jwt.LTS
{
    /// <summary>
    /// 数据类，用来帮助我们在应用的各个地方获取加密相关的信息：
    /// </summary>
    public class JWTTokenOptions
    {
        public string Audience { get; set; }
        public RsaSecurityKey Key { get; set; }
        public SigningCredentials Credentials { get; set; }
        public string Issuer { get; set; }
    }
}
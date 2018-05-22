using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json; 

namespace ECP.Util.Jwt.LTS
{
    public static class RSAUtils
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="withPrivate"></param>
        /// <param name="keyParameters"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool withPrivate, out RSAParameters keyParameters)
        {
            string filename = withPrivate ? "key.json" : "key.public.json";
            keyParameters = default(RSAParameters);
            if (Directory.Exists(filePath) == false||File.Exists(Path.Combine(filePath, filename))==false) return false;

            RsaParameterStorage parameters =  JsonConvert.DeserializeObject<RsaParameterStorage>(File.ReadAllText(Path.Combine(filePath, filename)));
            keyParameters = new RSAParameters
            {
                D = parameters.D,
                DP = parameters.DP,
                DQ = parameters.DQ,
                Exponent = parameters.Exponent,
                InverseQ = parameters.InverseQ,
                Modulus = parameters.Modulus,
                P = parameters.P,
                Q = parameters.Q
            };
            return true;
        }

        /// <summary>
        /// 生成并保存 RSA 公钥与私钥
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <returns></returns>
        public static RSAParameters GenerateAndSaveKey(string filePath)
        {
            RSAParameters publicKeys, privateKeys;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    privateKeys = rsa.ExportParameters(true);
                    publicKeys = rsa.ExportParameters(false);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            File.WriteAllText(Path.Combine(filePath, "key.json"),  privateKeys.ToJsonString());
            File.WriteAllText(Path.Combine(filePath, "key.public.json"), publicKeys.ToJsonString());
            return privateKeys;
        }



        // 转换成 json 字符串
        static string ToJsonString(this RSAParameters parameters)
        {
            var content = new RsaParameterStorage(); 
            content.D = parameters.D;
            content.DP = parameters.DP;
            content.DQ = parameters.DQ;
            content.Exponent = parameters.Exponent;
            content.InverseQ = parameters.InverseQ;
            content.Modulus = parameters.Modulus;
            content.P = parameters.P;
            content.Q = parameters.Q;  
            return JsonConvert.SerializeObject(content);
        }
    }

    class RsaParameterStorage
    {
        public byte[] D { get; set; }
        public byte[] DP { get; set; }
        public byte[] DQ { get; set; }
        public byte[] Exponent { get; set; }
        public byte[] InverseQ { get; set; }
        public byte[] Modulus { get; set; }
        public byte[] P { get; set; }
        public byte[] Q { get; set; }
    }
}
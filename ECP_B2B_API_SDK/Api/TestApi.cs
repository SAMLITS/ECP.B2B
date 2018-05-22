using ECP_B2B_API_SDK.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP_B2B_API_SDK.Api
{
    public class TestApi
    {
        public static void Test(RequestEntity requestEntity)
        {
            requestEntity.bodyParams = new Dictionary<string, object>() { {"2",1 } };
            requestEntity.url = "/account/user/ecp_login";
            HttpClientHelper.SendPostAsync(requestEntity);
        }
    }
}

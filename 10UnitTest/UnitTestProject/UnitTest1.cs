using ECP.B2b.ComEntity;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClient.System;
using Grpc.Core; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Channel channel = new Channel("127.0.0.1:9007", ChannelCredentials.Insecure);
            var client = new ECP.Util.ConfigDc.ProtoProxy.ConfigDcUtil.ConfigDcUtilClient(channel);
            var serverRes = client.GetDbConnectionConfig(new ECP.Util.ConfigDc.ProtoProxy.DbConfigRequest());
           string vl =  serverRes.DbConfigVal;
        }
        

    }
}

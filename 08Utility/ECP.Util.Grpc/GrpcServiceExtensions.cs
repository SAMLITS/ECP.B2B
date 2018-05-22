using Grpc.Core;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ECP.Util.Grpc
{
    public class GrpcServiceExtensions
    {
        public static Method<Request, Response> BuildMethod<Request, Response>(string serviceName, string handlerName, MethodType type = MethodType.Unary)
        {
            return new Method<Request, Response>(
                type,
                serviceName,
                handlerName,
                Marshallers.Create(ProtobufExtensions.Serialize, ProtobufExtensions.Derialize<Request>),
                Marshallers.Create(ProtobufExtensions.Serialize, ProtobufExtensions.Derialize<Response>)
            );
        }
    }

    internal class ProtobufExtensions
    {
        public static byte[] Serialize<T>(T input)
        {
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, input);
                return ms.ToArray();
            }
        }

        public static T Derialize<T>(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }

}

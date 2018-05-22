using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ECP.Util.Grpc
{
    public class ProtoBufBaseClient : ClientBase<ProtoBufBaseClient>
    {
        /// <summary>Creates a new client for Greeter</summary>
        /// <param name="channel">The channel to use to make remote calls.</param>
        public ProtoBufBaseClient(Channel channel) : base(channel)
        {
        }
        protected ProtoBufBaseClient(ClientBaseConfiguration configuration) : base(configuration)
        {
        }

        public Response MethodTemp<Request, Response>(Request request, Method<Request, Response> _Method) where Request : class where Response : class
        {
            Metadata headers = null;
            DateTime? deadline = null;
            CancellationToken cancellationToken = default(CancellationToken);

            return MethodTemp(request, new CallOptions(headers, deadline, cancellationToken), _Method);
        }
        public Response MethodTemp<Request, Response>(Request request, CallOptions options, Method<Request, Response> _Method) where Request : class where Response : class
        {
            return CallInvoker.BlockingUnaryCall(_Method, null, options, request);
        }


        protected override ProtoBufBaseClient NewInstance(ClientBaseConfiguration configuration)
        {
            return new ProtoBufBaseClient(configuration);
        }
    }
}

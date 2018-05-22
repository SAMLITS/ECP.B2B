using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.ModelDto.System.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using ECP.Util.Grpc;
using System.Threading.Tasks;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Main.GrpcProxy.System
{
    public static class MessagesProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_MESSAGES> baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>(typeof(MessagesProxy));


        //单独服务方法在此 Method 定义.....
        public readonly static Method<NullableParams, string> __Method_GetMaxMessagesNumber = GrpcServiceExtensions.BuildMethod<NullableParams, string>(baseProxy.__ServiceName, "GetMaxMessagesNumber");

        public readonly static Method<string, MessageAlertDto> __Method_FindMessageByAlert = GrpcServiceExtensions.BuildMethod<string, MessageAlertDto>(baseProxy.__ServiceName, "FindMessageByAlert");



        public static ServerServiceDefinition BindService(IMessagesProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....
            builder
                .AddMethod(__Method_FindMessageByAlert, serviceImpl.FindMessageByAlert)
                .AddMethod(__Method_GetMaxMessagesNumber, serviceImpl.GetMaxMessagesNumber);
            return builder.Build();
        }

        public interface IMessagesProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>
        {
            //单独服务方法在此扩展  .....
            Task<MessageAlertDto> FindMessageByAlert(string request, ServerCallContext context);
            Task<string> GetMaxMessagesNumber(NullableParams request, ServerCallContext context);
        }


        public class MessagesProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>
        {
            public MessagesProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                FindMessageByAlert = (r) => base.MethodTemp(r, __Method_FindMessageByAlert);
                GetMaxMessagesNumber = (r) => base.MethodTemp(r, __Method_GetMaxMessagesNumber);

            }

            //单独服务方法在此扩展  .....
            public Func<string, MessageAlertDto> FindMessageByAlert;
            public Func<NullableParams, string> GetMaxMessagesNumber;
        }

    }
}


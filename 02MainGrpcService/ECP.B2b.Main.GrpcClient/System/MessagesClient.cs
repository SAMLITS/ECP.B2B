using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.Messages;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 消息客户端实现类
    /// </summary>
    public class MessagesClient : BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>,Interface.System.IMessagesClient
    {
        private Func<Channel, MessagesProxy.MessagesProxyClient> _messageProxyClient;
        public MessagesClient() : base((Channel channel) => new MessagesProxy.MessagesProxyClient(channel)._ReturnThis())
        {
            _messageProxyClient = (Channel channel) => new MessagesProxy.MessagesProxyClient(channel);
        }
         
        public Task<MessageAlertDto> FindMessageByAlert(string msgNumber)
        {

            return Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "FindMessageByAlert").Result;
                var client = this._messageProxyClient(channel);
                var serverRes = client.FindMessageByAlert(msgNumber);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }

        /// <summary>
        /// 获取消息编号最大值
        /// </summary>
        /// <returns></returns>
        public Task<string> GetMaxMessagesNumber()
        {

            return Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "GetMaxMessagesNumber").Result;
                var client = this._messageProxyClient(channel);
                NullableParams nullableParams = null;
                var serverRes = client.GetMaxMessagesNumber(nullableParams);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

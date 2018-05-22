using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.Messages;
using ECP.B2b.Service.Interface;
using ECP.B2b.Service.Interface.System;
using ECP.Util.Common;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcService
{
    public class MessagesProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_MESSAGES>, MessagesProxy.IMessagesProxyBase
    {
        private Func<IB2B_MESSAGES_Service> _messagesService;
        public MessagesProxyService(Func<IB2B_MESSAGES_Service> messagesService) : base(messagesService)
        {
            _messagesService = messagesService;
            base._ListByPageSelector = m => new B2B_MESSAGES
            {
                ID = m.ID,
                MESSAGE_NUMBER = m.MESSAGE_NUMBER,
                MESSAGE_NAME = m.MESSAGE_NAME,
                MESSAGE_TEXT = m.MESSAGE_TEXT,
                MESSAGE_TYPE = m.MESSAGE_TYPE,
                MESSAGE_DESCRIPTION = m.MESSAGE_DESCRIPTION,

                CREATION_DATE = m.CREATION_DATE,
                CREATOR = m.CREATOR,
                LAST_UPDATE_DATE = m.LAST_UPDATE_DATE,
                EDITOR = m.EDITOR,
            };
        }

        public Task<MessageAlertDto> FindMessageByAlert(string request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                List<B2B_MESSAGES> messageList = this.baseService().FindAll(m => m.MESSAGE_NUMBER == request, m => new B2B_MESSAGES
                {
                    MESSAGE_NUMBER = m.MESSAGE_NUMBER,
                    MESSAGE_TEXT = m.MESSAGE_TEXT,
                    MESSAGE_TYPE = m.MESSAGE_TYPE
                }).Result;

                if (messageList.Count > 0)
                    return EntityAutoMapper.ConvertMapping<MessageAlertDto, B2B_MESSAGES>(messageList[0]);
                else
                    return new MessageAlertDto();
            });
        }

        public Task<string> GetMaxMessagesNumber(NullableParams request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                return _messagesService().GetMaxMessagesNumber();
            });
        }
    }
}

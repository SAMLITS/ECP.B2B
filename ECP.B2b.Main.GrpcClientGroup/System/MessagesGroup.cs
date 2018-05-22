using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.Messages;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.System
{
    public class MessagesGroup: IMessagesGroup
    {
        public IMessagesClient _messagesClient;
        public ILookUpValuesClient _lookUpValuesClient;

        public MessagesGroup(IMessagesClient messagesClient, ILookUpValuesClient lookUpValuesClient)
        {
            _messagesClient = messagesClient;
            _lookUpValuesClient = lookUpValuesClient;
        }

        public async Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams)
        {
            var messagesList = _messagesClient.GetListByPage(queryParams);
            var lookupList = _lookUpValuesClient.GetLookUpValuesByType(new List<ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams>
            {
                new ComEntity.Filter.LookUpValues.LookUpValuesByTypeParams{ IsBetweenOt=true, LookUpName ="XXECP_MESSAGE_TYPE"}
            });

            await messagesList;
            await lookupList;

            var result = from m in messagesList.Result.Data
                         join p in lookupList.Result.Where(l => l.LOOKUP_TYPE == "XXECP_MESSAGE_TYPE")
                                     on m.MESSAGE_TYPE equals p.LOOKUP_CODE into temp
                         from tt in temp.DefaultIfEmpty()
                         select new PageResultReplyDto
                         {
                             ID = m.ID,
                             MESSAGE_NUMBER = m.MESSAGE_NUMBER,
                             MESSAGE_NAME = m.MESSAGE_NAME,
                             MESSAGE_TEXT = m.MESSAGE_TEXT,
                             MESSAGE_TYPE = m.MESSAGE_TYPE,
                             MESSAGE_TYPE_NAME = tt==null? m.MESSAGE_TYPE:tt.LOOKUP_MEANING,
                             MESSAGE_DESCRIPTION=m.MESSAGE_DESCRIPTION,
                             CREATION_DATE= m.CREATION_DATE,
                             CREATOR= m.CREATOR,
                             LAST_UPDATE_DATE= m.LAST_UPDATE_DATE,
                             EDITOR= m.EDITOR
                         };
            messagesList.Result.Data = result.ToList();
            return messagesList.Result;
        }
    }
}

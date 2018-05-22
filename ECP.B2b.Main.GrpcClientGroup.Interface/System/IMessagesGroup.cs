using ECP.B2b.ComEntity.Filter.Messages;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.Interface.System
{
    public interface IMessagesGroup
    {
        Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams);
    }
}

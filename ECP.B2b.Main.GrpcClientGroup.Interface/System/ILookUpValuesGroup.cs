using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.LookUpValues;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.Interface.System
{
    public interface ILookUpValuesGroup
    {
        Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams);
    }
}

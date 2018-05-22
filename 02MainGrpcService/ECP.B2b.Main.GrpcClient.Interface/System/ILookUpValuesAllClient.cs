using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.LookUpValuesAll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ECP.B2b.ComEntity.Filter.LookUpValuesAll.PageQueryParams;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    /// <summary>
    /// 码表客户端接口
    /// </summary>
    public interface ILookUpValuesAllClient : IBaseGrpcClient<PageResultReplyDto, ComEntity.Filter.LookUpValuesAll.PageQueryParams, B2B_LOOKUP_VALUES_ALL>
    { 
    }
}

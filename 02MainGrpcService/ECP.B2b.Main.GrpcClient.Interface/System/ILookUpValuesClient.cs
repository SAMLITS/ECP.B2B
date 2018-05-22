using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.LookUpValues;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ECP.B2b.ComEntity.Filter.LookUpValues.PageQueryParams;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    /// <summary>
    /// 码表明细客户端接口
    /// </summary>
    public interface ILookUpValuesClient : IBaseGrpcClient<PageResultReplyDto, ComEntity.Filter.LookUpValues.PageQueryParams, B2B_LOOKUP_VALUES>
    {
        /// <summary>
        /// 获取码表明细
        /// </summary>
        /// <param name="selLookUpValuesparams"></param>
        /// <returns></returns>
        Task<List<LookUpValuesByTypeDto>> GetLookUpValuesByType(List<LookUpValuesByTypeParams> requests);
    }
}

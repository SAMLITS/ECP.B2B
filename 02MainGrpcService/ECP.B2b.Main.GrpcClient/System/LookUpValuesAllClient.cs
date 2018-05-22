using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.Main.GrpcClient.Interface.System;
using System.Threading.Tasks;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.LookUpValuesAll;
using static ECP.B2b.ComEntity.Filter.LookUpValuesAll.PageQueryParams;
using Grpc.Core;
using ECP.Util.Grpc;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ComEntity.Filter.LookUpValuesAll;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 码表客户端实现类
    /// </summary>
    public class LookUpValuesAllClient: BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL>, Interface.System.ILookUpValuesAllClient
    {
        public LookUpValuesAllClient() : base((Channel channel) => new LookUpValuesAllProxy.LookUpValuesAllProxyClient(channel)._ReturnThis())
        {

        } 
    }
}

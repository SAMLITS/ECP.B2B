using System;
using System.Collections.Generic;
using System.Text;
using ECP.B2b.Main.GrpcClient.Interface.System;
using System.Threading.Tasks;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.LookUpValues;
using static ECP.B2b.ComEntity.Filter.LookUpValues.PageQueryParams;
using Grpc.Core;
using ECP.Util.Grpc;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity;

namespace ECP.B2b.Main.GrpcClient
{
    /// <summary>
    /// 码表明细客户端实现类
    /// </summary>
    public class LookUpValuesClient: BaseGrpcClient<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES>, Interface.System.ILookUpValuesClient
    {
        private Func<Channel, LookUpValuesProxy.LookUpValuesProxyClient> _LookUpValuesProxyClient;
        public LookUpValuesClient() : base((Channel channel) => new LookUpValuesProxy.LookUpValuesProxyClient(channel)._ReturnThis())
        {
            _LookUpValuesProxyClient= (Channel channel) => new LookUpValuesProxy.LookUpValuesProxyClient(channel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getSelLookUpValuesParams"></param>
        /// <returns></returns>
        public Task<List<LookUpValuesByTypeDto>> GetLookUpValuesByType(List<LookUpValuesByTypeParams> requests)
        {
            return Task.Run(() =>
            {
                Channel channel = ClientFindCreateChannel.CreateChannel(_Mname, "GetLookUpValuesByType").Result;
                var client = this._LookUpValuesProxyClient(channel);
                var serverRes = client.GetLookUpValuesByType(requests);
                channel.ShutdownAsync();   //关闭长连接
                return serverRes;
            });
        }
    }
}

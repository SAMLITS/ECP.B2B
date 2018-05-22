using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.LookUpValues;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy.System
{
    public static class LookUpValuesProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES> baseProxy= new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES>(typeof(LookUpValuesProxy));
       
        //单独服务方法在此 Method 定义.....
        public readonly static Method<List<LookUpValuesByTypeParams>, List<LookUpValuesByTypeDto>> __Method_GetLookUpValuesByType = GrpcServiceExtensions.BuildMethod<List<LookUpValuesByTypeParams>, List<LookUpValuesByTypeDto>>(baseProxy.__ServiceName, "GetLookUpValuesByType");



        public static ServerServiceDefinition BindService(ILookUpValuesProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....

            builder.AddMethod(__Method_GetLookUpValuesByType, serviceImpl.GetLookUpValuesByType);
            return builder.Build();
        }

        public interface ILookUpValuesProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES>
        {
            //单独服务方法在此扩展  .....
            /// <summary>
            /// 查询码表明细
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            Task<List<LookUpValuesByTypeDto>> GetLookUpValuesByType(List<LookUpValuesByTypeParams> requests, ServerCallContext context);
        }


        public class LookUpValuesProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES>
        {
            public LookUpValuesProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....
                GetLookUpValuesByType = (r) => base.MethodTemp(r, __Method_GetLookUpValuesByType);
            }

            //单独服务方法在此扩展  .....
            public Func<List<LookUpValuesByTypeParams>, List<LookUpValuesByTypeDto>> GetLookUpValuesByType;
        }
    }
}

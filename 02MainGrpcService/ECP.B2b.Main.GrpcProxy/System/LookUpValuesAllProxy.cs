using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValuesAll;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto.System.LookUpValuesAll;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy.System
{
    public static class LookUpValuesAllProxy
    {
        static BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL> baseProxy;
        static LookUpValuesAllProxy()
        {
            baseProxy = new BaseProtoBufProxy<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL>(typeof(LookUpValuesAllProxy));
        }
        //单独服务方法在此 Method 定义.....



        public static ServerServiceDefinition BindService(ILookUpValuesAllProxyBase serviceImpl)
        {
            var builder = baseProxy.InitBaseService(serviceImpl);
            //单独服务方法在此 AddMethod 扩展.....


            return builder.Build();
        }

        public interface ILookUpValuesAllProxyBase : IEntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL>
        {
            //单独服务方法在此扩展  .....

        }


        public class LookUpValuesAllProxyClient : EntityProxyClient<PageResultReplyDto, PageQueryParams, B2B_LOOKUP_VALUES_ALL>
        {
            public LookUpValuesAllProxyClient(Channel channel) : base(channel, baseProxy)
            {
                //单独服务方法在此扩展  .....

            }

            //单独服务方法在此扩展  .....
        }
    }
}

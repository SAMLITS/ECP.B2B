using ECP.B2b.ComEntity.Page;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcProxy
{
    /********************** 查询控件查询方式代理级别扩展 ***********************/


    /// <summary>
    /// 查询控件调用 Proxy 提取
    /// </summary>
    /// <typeparam name="PD">分页DTO</typeparam>
    /// <typeparam name="PP">分页Filter</typeparam>
    /// <typeparam name="M">操作上下文实体</typeparam>
    /// <typeparam name="QPD">查询控件分页DTO</typeparam>
    /// <typeparam name="QPP">查询控件分页Filter</typeparam>
    public class BaseProtoExtendCQueryProxy<PD, PP, M,QPD,QPP>: BaseProtoBufProxy<PD, PP, M> where PD : class where PP : class where M : class where QPD : class where QPP : class 
    {
        public BaseProtoExtendCQueryProxy(Type serviceType):base(serviceType)
        { 
            __Method_GetCQueryListByPage = GrpcServiceExtensions.BuildMethod<QPP, PageResult<QPD>>(__ServiceName, "GetCQueryListByPage"); 
        } 

        public readonly Method<QPP, PageResult<QPD>> __Method_GetCQueryListByPage; 


        public  ServerServiceDefinition.Builder InitBaseService(IEntityProxyBaseExtendCQuery<PD, PP, M, QPD, QPP> serviceImpl)
        {
            var builder = base.InitBaseService(serviceImpl);
            builder.AddMethod(__Method_GetCQueryListByPage, serviceImpl.GetCQueryListByPage);
            return builder;
        }

    }
    /// <summary>
    /// 查询控件 基础声明服务
    /// </summary>
    public interface IEntityProxyBaseExtendCQuery<PD, PP, M, QPD, QPP> : IEntityProxyBase<PD, PP, M> where PD : class where PP : class where M : class where QPD : class where QPP : class
    {
        Task<PageResult<QPD>> GetCQueryListByPage(QPP request, ServerCallContext context);
    }
    /// <summary>
    /// 查询控件 基础客户端
    /// </summary>
    /// <typeparam name="PD"></typeparam>
    /// <typeparam name="PP"></typeparam>
    /// <typeparam name="M"></typeparam>
    public class EntityProxyExtendCQueryClient<PD, PP, M, QPD, QPP> : EntityProxyClient<PD, PP, M> where PD : class where PP : class where M : class where QPD : class where QPP : class
    {
        public EntityProxyExtendCQueryClient(Channel channel, BaseProtoExtendCQueryProxy<PD, PP, M, QPD, QPP> baseProxy) : base(channel, baseProxy)
        {
            GetCQueryListByPage = (r) => base.MethodTemp(r, baseProxy.__Method_GetCQueryListByPage); 

        }

        public Func<QPP, PageResult<QPD>> GetCQueryListByPage;  

        public new EntityProxyExtendCQueryClient<PD, PP, M, QPD, QPP> _ReturnThis()
        {
            return this;
        }
    }
}

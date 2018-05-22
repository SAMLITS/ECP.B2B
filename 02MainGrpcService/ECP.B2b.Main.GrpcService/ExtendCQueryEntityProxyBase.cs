using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.Main.GrpcProxy;
using ECP.B2b.ModelDto.System.Menu;
using ECP.B2b.Service.Interface;
using ECP.Util.Common;
using ECP.Util.Common.EFHelper;
using ECP.Util.Grpc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcService
{
    /// <summary>
    /// 支持查询控件 的基类  包含绝对基类
    /// </summary>
    /// <typeparam name="PD"></typeparam>
    /// <typeparam name="PP"></typeparam>
    /// <typeparam name="M"></typeparam>
    /// <typeparam name="QPD"></typeparam>
    /// <typeparam name="QPP"></typeparam>
    public abstract class ExtendCQueryEntityProxyBase<PD, PP, M, QPD, QPP> : EntityProxyBase<PD, PP, M> , IEntityProxyBaseExtendCQuery<PD, PP, M, QPD, QPP> where PD : class where PP : class where M : BaseModel,new() where QPD : class where QPP : class
    { 
        public new Logger logger = new Logger(typeof(ExtendCQueryEntityProxyBase<PD, PP, M, QPD, QPP>));
        public bool IsCAutoSelector = true;
        public ExtendCQueryEntityProxyBase(Func<IBaseService<M>> _baseService):base(_baseService)
        { 
        }

        /// <summary>
        /// 查询控件 分页方法 select 表达树
        /// </summary>
        protected Expression<Func<M, M>> _CQueryListByPageSelector { get; set; } = null; 

        public Task<PageResult<QPD>> GetCQueryListByPage(QPP request, ServerCallContext context)
        {
            return baseComMethod.GetListByPage<QPD, QPP>(request, CExtendQueryListByPage, IsAutoSelector, _CQueryListByPageSelector); 
        }
        /// <summary>
        /// 分页扩展 lamada 查询   子类重写
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public virtual IQueryable<M> CExtendQueryListByPage(IQueryable<M> queryable, QPP request)
        {
            return queryable;
        }

    }
}

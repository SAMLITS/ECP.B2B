using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel;
using ECP.Util.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECP.B2b.Service.Interface
{
    /// <summary>
    /// 基本操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> : IBaseTransService where T : BaseModel
    {
        /// <summary>
        /// 根据id查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Find(int id);

        /// <summary>
        /// 根据id查询实体  select 筛选
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Find(int id, Expression<Func<T, T>> selector);

        /// <summary>
        /// 根据条件 一次性查出全部数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> FindAll(Expression<Func<T, bool>> func = null, Expression<Func<T, T>> selector=null);
        /// <summary>
        /// selector 必须
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <param name="func"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        Task<List<D>> FindAll<D>(Expression<Func<T, D>> selector, Expression<Func<T, bool>> func = null);




        /// <summary>
        /// 新增数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        Task<T> Insert(T t);

        /// <summary>
        /// 批量新增数据，即时Commit
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        Task<List<T>> InsertRange(List<T> ts);



        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        Task<bool> Update(T t);
        /// <summary>
        /// 批量更新数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> UpdateRange(List<T> t);

        /// <summary>
        /// 更新数据，即时Commit 根据ModelModifyFlag特性自动去填充更新
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> UpdateModel(T t);

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        Task<bool> Delete(T t);

        /// <summary>
        /// 根据主键删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        Task<bool> Delete(int Id);
        /// <summary>
        /// 根据主键批量删除数据，即时Commit
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        Task<bool> DeleteRange(List<int> idList);



        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        Task<List<T>> PageQuery(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, T>> selector = null);

        Task<List<D>> PageQuery<D>(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, D>> selector);

        /// <summary>
        /// 根据主键软删除数据 仅修改删除标识
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> DeleteByFlag(RemoveModel rModel);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<int> Count(Expression<Func<T, bool>> func);
    }
}

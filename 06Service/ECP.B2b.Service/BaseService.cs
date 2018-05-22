using ECP.Util.Common.Extensions;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DAL.Interface;
using ECP.B2b.DbModel;
using ECP.B2b.Service.Interface;
using ECP.Util.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ECP.B2b.Service
{
    public class BaseService<T> : BaseTransService, IBaseService<T> where T : BaseModel
    {
        #region Identity
        public IBaseDal<T> ibaseDal;
        public BaseService(IBaseDal<T> _ibaseDal)
        {
            this.ibaseDal = _ibaseDal;
        }
        public BaseService(IBaseDal<T> _ibaseDal, IBaseTransDal _ibaseTranDal) : base(_ibaseTranDal)
        {
            this.ibaseDal = _ibaseDal;
        }
        #endregion Identity

        #region 基础方法
        public async Task<T> Insert(T t)
        {
            return await Task.Run<T>(() => ibaseDal.Insert(t));
        }

        public async Task<List<T>> InsertRange(List<T> ts)
        {
            return await Task.Run<List<T>>(() => ibaseDal.InsertRange(ts));
        }

        public async Task<T> Find(int id)
        {
            return await Task.Run<T>(() => ibaseDal.Find(id));
        }

        public async Task<T> Find(int id, Expression<Func<T, T>> selector)
        {
            return await Task.Run<T>(() => ibaseDal.Find(id, selector));
        }

        public async Task<List<T>> FindAll(Expression<Func<T, bool>> func = null, Expression<Func<T, T>> selector = null)
        {
            return await Task.Run<List<T>>(() => ibaseDal.FindAll(func, selector));
        }
        public async Task<List<D>> FindAll<D>(Expression<Func<T, D>> selector, Expression<Func<T, bool>> func = null)
        {
            return await Task.Run<List<D>>(() => ibaseDal.FindAll(selector, func));
        }

        public async Task<bool> Update(T t)
        {
            return await Task.Run<bool>(() => ibaseDal.Update(t));
        }
        public async Task<bool> UpdateRange(List<T> ts)
        {
            return await Task.Run<bool>(() => ibaseDal.UpdateRange(ts));
        }

        public async Task<bool> Delete(T t)
        {
            return await Task.Run<bool>(() => ibaseDal.Delete(t));
        }

        public async Task<bool> Delete(int Id)
        {
            return await Task.Run<bool>(() => ibaseDal.Delete(Id));
        }
        public async Task<bool> DeleteRange(List<int> idList)
        {
            return await Task.Run<bool>(() => ibaseDal.DeleteRange(idList));
        }


        public async Task<List<T>> PageQuery(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, T>> selector = null)
        {
            return await Task.Run<List<T>>(() => ibaseDal.PageQuery(func, pageInfo, selector));
        }

        public async Task<List<D>> PageQuery<D>(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, D>> selector)
        {
            return await Task.Run<List<D>>(() => ibaseDal.PageQuery<D>(func, pageInfo, selector));
        }

        public async Task<bool> DeleteByFlag(RemoveModel rModel)
        {
            return await Task.Run<bool>(() => ibaseDal.DeleteByFlag(rModel));
        }

        public async Task<bool> UpdateModel(T t)
        {
            T _DestModel = ibaseDal.Find(t.ID);
            t.SourceDestModifyConvert(_DestModel);
            return await Task.Run<bool>(() => ibaseDal.Update(_DestModel));
        }

        public async Task<int> Count(Expression<Func<T, bool>> func)
        {
            return await Task.Run<int>(() => ibaseDal.Count(func));
        }

       

        #endregion
    }
}

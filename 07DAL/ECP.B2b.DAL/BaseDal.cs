using ECP.B2b.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using ECP.Util.Common;
using System.Threading.Tasks;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.DbModel;
using ECP.B2b.ComEntity;

namespace ECP.B2b.DAL
{
    public class BaseDal<T> : BaseTransDal, IBaseDal<T> where T : BaseModel
    {
        #region Identity
        public DbContext Context { get; set; }
        protected DbSet<T> TDbSet;

        public BaseDal(DbContext hbContext):base(hbContext)
        {
            this.Context = hbContext;
            this.TDbSet = this.Context.Set<T>();
        }
        #endregion Identity

        #region 基础方法
        public T Insert(T t)
        {
            t.DEL_FLAG =0;
            this.TDbSet.Add(t);
            this.Commit();
            return t;
        }


        public List<T> InsertRange(List<T> ts)
        { 
            this.TDbSet.AddRange(ts);
            this.Commit();
            return ts;
        }



        public T Find(int id)
        {
            return this.TDbSet.Where(t => t.DEL_FLAG == 0 && t.ID == id).First();
        }

        public T Find(int id, Expression<Func<T, T>> selector)
        {
            return this.TDbSet.Where(t => t.DEL_FLAG == 0 && t.ID == id).Select(selector).First();
        }


        public List<T> FindAll(Expression<Func<T, bool>> func = null, Expression<Func<T, T>> selector = null)
        {
            return this.TDbSet == null ? null
                : selector != null && func != null ? TDbSet.Where(t => t.DEL_FLAG == 0).Where(func).Select(selector).ToList()
                : selector != null ? TDbSet.Where(t => t.DEL_FLAG == 0).Select(selector).ToList()
                : func != null ? TDbSet.Where(t => t.DEL_FLAG == 0).Where(func).ToList()
                : TDbSet.Where(t => t.DEL_FLAG == 0).ToList();
        }
        public List<D> FindAll<D>(Expression<Func<T, D>> selector, Expression<Func<T, bool>> func = null)
        {
            return this.TDbSet == null ? null
                : func != null ? TDbSet.Where(t => t.DEL_FLAG == 0).Where(func).Select(selector).ToList()
                : TDbSet.Where(t => t.DEL_FLAG == 0).Select(selector).ToList();
        }


        public IQueryable<T> Set()
        {
            return this.TDbSet;
        }

        public bool Update(T t)
        {
            if (t == null) throw new Exception("t is null"); 

            var entry = this.Context.Entry(t);
            this.TDbSet.Attach(t);
            entry.State = EntityState.Modified;
            return this.Commit();
        }
        public bool UpdateRange(List<T> ts)
        {
            if (ts == null) throw new Exception("ts is null");
            this.TDbSet.UpdateRange(ts);
            return this.Commit();
        }

        public bool Delete(T t)
        {
            if (t == null) throw new Exception("t is null");
            this.TDbSet.Attach(t);
            this.TDbSet.Remove(t);
            return this.Commit();
        }

        public bool Delete(int Id)
        {
            T t = this.Find(Id);
            if (t == null) throw new Exception("t is null");
            this.TDbSet.Remove(t);
            return this.Commit();
        }

        public bool DeleteRange(List<int> idList)
        {
            var t = this.FindAll(f => idList.Contains(f.ID));
            if (t == null) throw new Exception("t is null");
            this.TDbSet.RemoveRange(t);
            return this.Commit();
        }

        public bool Commit()
        {
            return this.Context.SaveChanges() > 0;
        }


        public List<T> PageQuery(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, T>> selector = null)
        {
            IQueryable<T> queryable = func(this.TDbSet.Where<T>(ft => ft.DEL_FLAG == 0));
            //获取满足条件的数据数量
            pageInfo.TotalPageCount = queryable.Count();

            //排序
            if (!string.IsNullOrEmpty(pageInfo.SortOrder))
            {
                if (pageInfo.SortOrder.ToLower() == "desc")
                    queryable = queryable.OrderByDescendingProp(pageInfo.SortName);
                else
                    queryable = queryable.OrderByProp(pageInfo.SortName);
            }

            //分页
            queryable = queryable.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);

            //指定查询字段
            if (selector != null)
                queryable = queryable.Select(selector);

            return queryable.ToList();

            //return this.TDbSet.OrderByDescending(p => p.ID).Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
        }

        public List<D> PageQuery<D>(Func<IQueryable<T>, IQueryable<T>> func, PageFilter pageInfo, Expression<Func<T, D>> selector)
        {
            IQueryable<T> queryable = func(this.TDbSet.Where<T>(ft => ft.DEL_FLAG == 0));
            //获取满足条件的数据数量
            pageInfo.TotalPageCount = queryable.Count();

            //排序
            if (!string.IsNullOrEmpty(pageInfo.SortOrder))
            {
                if (pageInfo.SortOrder.ToLower() == "desc")
                    queryable = queryable.OrderByDescendingProp(pageInfo.SortName);
                else
                    queryable = queryable.OrderByProp(pageInfo.SortName);
            }

            //分页
            queryable = queryable.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);

            return queryable.Select(selector).ToList();
        }


        public bool DeleteByFlag(RemoveModel rModel)
        {
            T tModel = this.Find(rModel.ID);
            tModel.DEL_FLAG = 1;
            tModel.LAST_UPDATE_DATE = rModel.LAST_UPDATE_DATE;
            tModel.EDITOR = rModel.EDITOR;

            return this.Update(tModel);
        }

        public int Count(Expression<Func<T, bool>> func)
        {
            return this.TDbSet.Where(t => t.DEL_FLAG == 0).Count(func);
        }

        #endregion

        #region 子类中调用
        protected IQueryable<T> ExcuteQuery(string sql, Dictionary<string, string> dicParams)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            dicParams.ToList().ForEach(p =>
            {
                parameters.Add(new SqlParameter($"@{p.Key}", p.Value));
            });
            return this.Context.Set<T>().FromSql<T>(sql, parameters).AsQueryable();
        }
        protected bool Excute(string sql, Dictionary<string, string> dicParams)
        {
            IDbContextTransaction trans = null;
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                dicParams.ToList().ForEach(p =>
                {
                    parameters.Add(new SqlParameter($"@{p.Key}", p.Value));
                });

                trans = this.Context.Database.BeginTransaction();
                Context.Database.ExecuteSqlCommand(sql, parameters);
                trans.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
        }


        #endregion
    }
}

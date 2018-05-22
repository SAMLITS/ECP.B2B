using ECP.B2b.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ECP.B2b.AttributeModel;
using System.Reflection;
using ECP.B2b.DbModel;

namespace ECP.B2b.DAL
{
    public class BaseTransDal : IBaseTransDal
    {
        public DbContext _Context { get; set; }
        public BaseTransDal(DbContext hbContext)
        {
            this._Context = hbContext; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">加入事务类型</typeparam>
        /// <param name="t">加入事务对象</param>
        /// <param name="tArray">查找对象数组</param>

        private void TranBind<T>(T t,bool tIsJoin, params object[] tArray) where T : BaseModel
        {
            //如果要求关联才进行关联校验及赋值
            if (tIsJoin)
            {
                Type tType = typeof(T);
                List<PropertyInfo> tProperList = tType.GetProperties().Where(p => p.GetCustomAttribute(typeof(TransFieldBindAttribute)) != null).ToList();
                if (tProperList == null || tProperList.Count == 0)
                    throw new Exception(tType.FullName + "未找到事务关联属性，不需要-'多表关联事务处理业务'-请不要调用此事务处理方法！");


                //对外键绑定属性赋值
                foreach (var p in tProperList)
                {
                    TransFieldBindAttribute tfb = (TransFieldBindAttribute)p.GetCustomAttribute(typeof(TransFieldBindAttribute));
                    Type bindType = tfb._bindType;
                    string bindProperName = tfb._bindProperName;
                    List<object> mainObjList = tArray.Where(ta => ta.GetType() == bindType && ta.GetType().GetProperty(bindProperName) != null).ToList();

                    if (mainObjList.Count == 1)
                    {
                        p.SetValue(t, mainObjList[0].GetType().GetProperty(bindProperName).GetValue(mainObjList[0]));
                    }
                    else if (mainObjList.Count == 0)
                    {
                        throw new Exception($"{tType.FullName}未在目标关联类型{bindType.FullName}中找到目标关联字段{bindProperName}，请确认关联类型与关联字段设置是否正确，并且请确认传入参数顺序是否正确“主在前，从在后”！");
                    }
                    else
                    {
                        throw new Exception($"{tType.FullName}找到多个目标关联类型{bindType.FullName}，请确认调用方式是否正确！");
                    }
                };
            }

            this.SaveChangesT(t);
        }
        private void SaveChangesT<T>(T t) where T:BaseModel
        {
            if (t.ID <= 0)
            {
                _Context.Add(t);
            }
            else
            {
                _Context.Update(t);
            }
            _Context.SaveChanges();
        }



        public T1 DbTransaction<T1>(T1 t1) where T1 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    
                    SaveChangesT(t1);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            return (t1);
        }

        public (T1 t1, T2 t2) DbTransaction<T1, T2>(T1 t1, T2 t2, bool t2IsJoin = true) where T1 : BaseModel where T2 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);
                    TranBind(t2, t2IsJoin, t1);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2);
        }

        public (T1 t1, T2 t2, T3 t3) DbTransaction<T1, T2, T3>(T1 t1, T2 t2, T3 t3,   bool t2IsJoin = true, bool t3IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);

                    TranBind(t2, t2IsJoin, t1);
                    TranBind(t3, t3IsJoin, t1, t2);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2, t3);
        }

        public (T1 t1, T2 t2, T3 t3, T4 t4) DbTransaction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4,   bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);

                    TranBind(t2, t2IsJoin, t1);
                    TranBind(t3, t3IsJoin, t1, t2);
                    TranBind(t4, t4IsJoin, t1, t2, t3);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2, t3, t4);
        }

        public (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) DbTransaction<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5,  bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel where T5 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);

                    TranBind(t2, t2IsJoin, t1);
                    TranBind(t3, t3IsJoin, t1, t2);
                    TranBind(t4, t4IsJoin, t1, t2, t3);
                    TranBind(t5, t5IsJoin, t1, t2, t3, t4);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2, t3, t4, t5);
        }

        public (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) DbTransaction<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6,  bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true)
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
            where T5 : BaseModel
            where T6 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);

                    TranBind(t2, t2IsJoin, t1);
                    TranBind(t3, t3IsJoin, t1, t2);
                    TranBind(t4, t4IsJoin, t1, t2, t3);
                    TranBind(t5, t5IsJoin, t1, t2, t3, t4);
                    TranBind(t6, t6IsJoin, t1, t2, t3, t4, t5);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2, t3, t4, t5,t6);
        }

        public (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) DbTransaction<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true, bool t7IsJoin = true)
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
            where T5 : BaseModel
            where T6 : BaseModel
            where T7 : BaseModel
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    SaveChangesT(t1);

                    TranBind(t2, t2IsJoin, t1);
                    TranBind(t3, t3IsJoin, t1, t2);
                    TranBind(t4, t4IsJoin, t1, t2, t3);
                    TranBind(t5, t5IsJoin, t1, t2, t3, t4);
                    TranBind(t6, t6IsJoin, t1, t2, t3, t4, t5);
                    TranBind(t7, t7IsJoin, t1, t2, t3, t4, t5, t6);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return (t1, t2, t3, t4, t5, t6, t7);
        }

       
    }
}

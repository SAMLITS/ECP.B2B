using ECP.B2b.DAL.Interface;
using ECP.B2b.DbModel;
using ECP.B2b.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Service
{
    public class BaseTransService : IBaseTransService
    {
        #region Identity
        public IBaseTransDal ibaseTranDal;

        /// <summary>
        /// 如果是此构造实例的话 将代表不会使用自动事务，如果使用的话将报出null空指针错误，需要在Service实现子类中调用父类BaseService的两个参数的构造函数
        /// </summary>
        public BaseTransService()
        {

        }
        public BaseTransService(IBaseTransDal _ibaseTranDal)
        {
            this.ibaseTranDal = _ibaseTranDal;
        }
        #endregion Identity

        public async Task<T1> DbTransaction<T1>(T1 t1) where T1 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1));
        }

        public async Task<(T1 t1, T2 t2)> DbTransaction<T1, T2>(T1 t1, T2 t2, bool t2IsJoin = true )
            where T1 : BaseModel
            where T2 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t2IsJoin));
        }

        public async Task<(T1 t1, T2 t2, T3 t3)> DbTransaction<T1, T2, T3>(T1 t1, T2 t2, T3 t3, bool t2IsJoin = true, bool t3IsJoin = true )
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t3, t2IsJoin, t3IsJoin ));
        }

        public async Task<(T1 t1, T2 t2, T3 t3, T4 t4)> DbTransaction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true )
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t3, t4, t2IsJoin, t3IsJoin, t4IsJoin ));
        }

        public async Task<(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)> DbTransaction<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true )
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
            where T5 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t3, t4, t5, t2IsJoin, t3IsJoin, t4IsJoin, t5IsJoin ));
        }

        public async Task<(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)> DbTransaction<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true )
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
            where T5 : BaseModel
            where T6 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t3, t4, t5, t6, t2IsJoin, t3IsJoin, t4IsJoin, t5IsJoin, t6IsJoin ));
        }

        public async Task<(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)> DbTransaction<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true, bool t7IsJoin = true)
            where T1 : BaseModel
            where T2 : BaseModel
            where T3 : BaseModel
            where T4 : BaseModel
            where T5 : BaseModel
            where T6 : BaseModel
            where T7 : BaseModel
        {
            return await Task.Run(() => ibaseTranDal.DbTransaction(t1, t2, t3, t4, t5, t6, t7, t2IsJoin, t3IsJoin, t4IsJoin, t5IsJoin, t6IsJoin, t7IsJoin));
        }
    }
}

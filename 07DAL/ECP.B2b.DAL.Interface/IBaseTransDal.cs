using ECP.B2b.DbModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.DAL.Interface
{
    public interface IBaseTransDal
    {
        T1 DbTransaction<T1>(T1 t1) where T1 : BaseModel;

        (T1 t1, T2 t2) DbTransaction<T1,T2>(T1 t1, T2 t2 , bool t2IsJoin = true) where T1: BaseModel where T2: BaseModel;
        (T1 t1, T2 t2, T3 t3) DbTransaction<T1, T2, T3>(T1 t1, T2 t2, T3 t3 , bool t2IsJoin = true, bool t3IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel;
        (T1 t1, T2 t2, T3 t3, T4 t4) DbTransaction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4 , bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel;
        (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) DbTransaction<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5 , bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel where T5 : BaseModel;

        (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) DbTransaction<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6,  bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel where T5 : BaseModel where T6 : BaseModel;
        (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) DbTransaction<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, bool t2IsJoin = true, bool t3IsJoin = true, bool t4IsJoin = true, bool t5IsJoin = true, bool t6IsJoin = true, bool t7IsJoin = true) where T1 : BaseModel where T2 : BaseModel where T3 : BaseModel where T4 : BaseModel where T5 : BaseModel where T6 : BaseModel where T7 : BaseModel;
    }
}

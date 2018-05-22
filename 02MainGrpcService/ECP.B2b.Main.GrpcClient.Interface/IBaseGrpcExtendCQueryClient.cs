using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Page;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient.Interface
{
    public interface IBaseGrpcExtendCQueryClient<PD, PP, M, QPD, QPP> : IBaseGrpcClient<PD, PP, M> where PD : class where PP : class where M : class where QPD : class where QPP : class
    {
        Task<PageResult<QPD>> GetCQueryListByPage(QPP queryParams);
    }
}

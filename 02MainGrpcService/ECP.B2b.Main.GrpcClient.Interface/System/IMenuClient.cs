using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions; 
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.Menu; 
using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ComEntity.Filter.Menu;
using ECP.B2b.ComEntity.System;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    public interface IMenuClient: IBaseGrpcExtendCQueryClient<PageResultReplyDto, PageQueryParams, B2B_MENU, CQueryPageResultReplyDto, CQueryPageQueryParams>
    {
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<UserMenuResDto>> GetUserMenus(UserMenuReq request);
    }
}

using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.LookUpValues;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.ModelDto;
using ECP.B2b.ModelDto.System.UserMenu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClient.Interface.System
{
    public interface IUserMenuClient : IBaseGrpcClient<NullablePageResultReplyDto, NullableParams, B2B_USER_MENU>
    {
        Task<List<MenuByUserDto>> FindMenuByUser(IdModel request);

        Task<AjaxResult> SetMenuByUser(List<B2B_USER_MENU> request);
    }
}

using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.ComEntity.Page;
using System.Collections.Generic;
using ECP.B2b.DbExtendModel.System;
using ECP.B2b.ModelDto.System.User;
using System.Threading.Tasks;

namespace ECP.B2b.Main.GrpcClientGroup.Interface.System
{
    public interface IUserGroup
    {
        Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams);

        Task<B2B_USER_Extend> FindExtendById(int ID);

        /// <summary>
        /// 解除微信绑定
        /// </summary>
        /// <param name="unBindWxDic"></param>
        /// <returns></returns>
        Task<AjaxResult> UnBindWx(Dictionary<string, string> unBindWxDic);

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="pwdDic"></param>
        /// <returns></returns>
        Task<AjaxResult> ChangePwd(Dictionary<string, string> pwdDic);
    }
}

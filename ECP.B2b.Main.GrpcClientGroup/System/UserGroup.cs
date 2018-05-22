using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.ComEntity.Page;
using ECP.B2b.ModelDto.System.User;
using ECP.B2b.Main.GrpcClient.Interface.System;
using ECP.B2b.Main.GrpcClientGroup.Interface.System;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECP.B2b.Main.GrpcClient.Interface.Basic;
using ECP.B2b.ComEntity;
using ECP.Util.Common;
using ECP.B2b.DbModel.Sys;
using ECP.B2b.DbExtendModel.System;
using ECP.Util.Common.Extensions;
using ECP.B2b.ComEntity.Basic;

namespace ECP.B2b.Main.GrpcClientGroup.System
{
    public class UserGroup : IUserGroup
    {
        public IUserClient _userClient; 
        public ILookUpValuesClient _lookUpValuesClient; 
        public ExtendSearchGroup _extendSearchGroup;

        public UserGroup(IUserClient userClient,  ILookUpValuesClient lookUpValuesClient, ExtendSearchGroup extendSearchGroup)
        {
            _userClient = userClient; 
            _lookUpValuesClient = lookUpValuesClient; 
            _extendSearchGroup = extendSearchGroup;
        }

        /// <summary>
        /// 用户管理分页数据组合获取（外键关联值&&码值）
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<PageResult<PageResultReplyDto>> ListByPageGroup(PageQueryParams queryParams)
        {
            #region 外键表字段查询
            //交易方模糊搜索
            _extendSearchGroup.PartyIdByNameFind(out var partyList, queryParams.QueryParams, "PARTY_NAME", "PARTY_NAME", "PARTY_ID");
            if (partyList != null && partyList.Result.Count == 0)
                return new PageResult<PageResultReplyDto>();
            #endregion

            //查出用户管理分页数据
            var userList = await _userClient.GetListByPage(queryParams);
            if (userList.Data.Count == 0) return new PageResult<PageResultReplyDto>();

            #region 关联值获取
          
            //获取码表
            _extendSearchGroup.JoinSearchLookup(out var lookupList, "YES_NO", "REG_STATUS", "PARTY_TYPE");
             
            await partyList;
            await lookupList;
            #endregion

            //列表数据关联值
            userList.Data.Join(
             lookupList.Result,
             partyList.Result,
            #region 组合关联配置字符串
            //组合关联本表码值
             "lookup->IS_MAIN_NAME->YES_NO->IS_MAIN",
             "lookup->IS_WXIN_LOGIN_NAME->YES_NO->IS_WXIN_LOGIN",
             "lookup->REG_STATUS_NAME->REG_STATUS->REG_STATUS",
             "lookup->PARTY_TYPE_NAME->PARTY_TYPE->PARTY_TYPE",
            //组合关联交易方关联值
            "j1->PARTY_NAME->PARTY_ID->ID->NAME1"
            #endregion
            );

            return userList;
        }

        /// <summary>
        /// 获取用户扩展类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<B2B_USER_Extend> FindExtendById(int userId)
        {
            //获取用户记录
            var user = await _userClient.FindById(new IdModel { ID = userId });
            //扩展类转换
            var userExtend = EntityAutoMapper.ConvertMapping<B2B_USER_Extend, B2B_USER>(user); 
            //获取微信详情
            var wxFindDic = new Dictionary<string, string>();
            wxFindDic.Add("USER_ID", user.ID.ToString());  
            return userExtend;
        }

        /// <summary>
        /// 解除微信绑定
        /// </summary>
        /// <param name="unBindWxDic"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UnBindWx(Dictionary<string, string> unBindWxDic)
        {
            var userUnBindWxEntity = new UserUnBindWxEntity();
            //获取相关记录
            var user = _userClient.FindById(new IdModel { ID = Convert.ToInt32(unBindWxDic["USER_ID"]) }); 

            await user; 
            //判断是否强制微信登录
            if (user.Result.IS_WXIN_LOGIN == "Y")
            {
                userUnBindWxEntity.b2b_USER = user.Result;
            } 
            var ajaxResult = _userClient.UnBindWx(userUnBindWxEntity);
            return ajaxResult.Result;
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="pwdDic"></param>
        /// <returns></returns>
        public async Task<AjaxResult> ChangePwd(Dictionary<string, string> pwdDic)
        {
            //获取相关记录
            var user =await _userClient.FindById(new IdModel { ID = Convert.ToInt32(pwdDic["ID"]) });
            //判断原密码是否正确
            if (user.PASSWORD != pwdDic["ORIGINAL_PASSWORD"])
            {
                return new AjaxResult
                {
                    NumberMsg = 3114,
                    Result = DoResult.ValidError
                };
            }
            var reply =await _userClient.ModifyDic(pwdDic);
            if (reply.Result == DoResult.Success) reply.NumberMsg= 3115;
            return reply;
        }
    }
}

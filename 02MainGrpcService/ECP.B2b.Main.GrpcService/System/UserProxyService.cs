using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using ECP.B2b.DbModel.Sys;
using System.Linq;
using ECP.Util.Common;
using ECP.B2b.Main.GrpcProxy.System;
using ECP.B2b.ModelDto.System.User;
using ECP.B2b.ComEntity.Filter.User;
using ECP.B2b.ComEntity;
using ECP.B2b.ComEntity.CurrentUser;
using ECP.B2b.ModelDto;
using ECP.B2b.Service.Interface.Basic;
using ECP.B2b.ComEntity.Basic;

namespace ECP.B2b.Main.GrpcService
{
    public class UserProxyService : EntityProxyBase<PageResultReplyDto, PageQueryParams, B2B_USER>, UserProxy.IUserProxyBase
    {
        private Func<IB2B_USER_Service> _userService;
        public UserProxyService(Func<IB2B_USER_Service> userService) : base(userService)
        {
            this._userService = userService;
            base._ListByPageSelector = m => new B2B_USER
            {
                ID = m.ID,
                PARTY_ID = m.PARTY_ID,
                PARTY_TYPE = m.PARTY_TYPE,
                USER = m.USER,
                USER_NAME = m.USER_NAME,
                IS_MAIN = m.IS_MAIN,
                REG_STATUS = m.REG_STATUS,


                IS_WXIN_LOGIN = m.IS_WXIN_LOGIN,
                MOBILE = m.MOBILE,
                MAIL = m.MAIL,
                REMARK = m.REMARK,
                START_DATE = m.START_DATE,
                END_DATE = m.END_DATE,
                CREATION_DATE = m.CREATION_DATE,
                CREATOR = m.CREATOR,
                LAST_UPDATE_DATE = m.LAST_UPDATE_DATE,
                EDITOR = m.EDITOR,
                LAST_LOGIN_DATE = m.LAST_LOGIN_DATE
            };
        }



        public Task<NullableResult> LastLoginTimeReload(IdModel request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _userService().LastLoginTimeReload(request.ID);
                return new NullableResult();
            });
        }

        public Task<NullableResult> LoginOut(IdModel request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                _userService().LoginOut(request.ID);
                return new NullableResult();
            });
        }

        public Task<CurrentUserEntity> ManagerUserLogin(LoginEntity request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                List<B2B_USER> users = _userService().FindAll(u => u.USER == request.USER && u.PASSWORD == request.PASSWORD && new string[] { "SYS", "FIN" }.Contains(u.PARTY_TYPE), u => new B2B_USER
                {
                    ID = u.ID,
                    PARTY_ID = u.PARTY_ID,
                    PARTY_TYPE = u.PARTY_TYPE,
                    USER = u.USER,
                    IS_MAIN = u.IS_MAIN,
                    USER_NAME = u.USER_NAME,
                    MOBILE = u.MOBILE,
                    MAIL = u.MAIL,
                    IS_WXIN_LOGIN = u.IS_WXIN_LOGIN,
                    REG_STATUS = u.REG_STATUS,
                    START_DATE = u.START_DATE,
                    END_DATE = u.END_DATE
                }).Result;

                if (users != null && users.Count == 1)
                {
                    return EntityAutoMapper.ConvertMapping<CurrentUserEntity, B2B_USER>(users[0]);
                }
                else
                {
                    return new CurrentUserEntity();
                }
            });
        }
        /// <summary>
        /// 前台登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<CurrentUserEntity> UserLogin(LoginEntity request, ServerCallContext context)
        {
            return Task.Run(() =>
            {
                List<B2B_USER> users = _userService().FindAll(u => u.USER == request.USER && u.PASSWORD == request.PASSWORD && new string[] { "ECE" }.Contains(u.PARTY_TYPE), u => new B2B_USER
                {
                    ID = u.ID,
                    PARTY_ID = u.PARTY_ID,
                    PARTY_TYPE = u.PARTY_TYPE,
                    USER = u.USER,
                    IS_MAIN = u.IS_MAIN,
                    USER_NAME = u.USER_NAME,
                    MOBILE = u.MOBILE,
                    MAIL = u.MAIL,
                    IS_WXIN_LOGIN = u.IS_WXIN_LOGIN,
                    REG_STATUS = u.REG_STATUS,
                    START_DATE = u.START_DATE,
                    END_DATE = u.END_DATE
                }).Result;

                if (users != null && users.Count == 1)
                {
                    return EntityAutoMapper.ConvertMapping<CurrentUserEntity, B2B_USER>(users[0]);
                }
                else
                {
                    return new CurrentUserEntity();
                }
            });
        }

        /// <summary>
        /// 重写编辑保存服务
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<AjaxResult> ModifyDic(Dictionary<string, string> request, ServerCallContext context)
        {
            //处理密码保存，如编辑界面未填写密码，则不进行密码变更，否则变更密码以及密码更新时间
            if (request.Keys.Contains("PASSWORD") && string.IsNullOrEmpty(request["PASSWORD"]))
                request["PASSWORD"] = baseService().FindAll(x => x.ID == Convert.ToInt32(request["ID"]), u => new B2B_USER { PASSWORD = u.PASSWORD }).Result[0].PASSWORD;
            else if (request.Keys.Contains("PASSWORD_UPDATE_TIME"))
                request["PASSWORD_UPDATE_TIME"] = DateTime.Now.ToString();
            else
                request.Add("PASSWORD_UPDATE_TIME", DateTime.Now.ToString());
            return base.ModifyDic(request, context);
        }

        /// <summary>
        /// 重写编辑保存服务
        /// </summary>
        /// <param name="newModel"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<AjaxResult> Modify(B2B_USER newModel, ServerCallContext context)
        {
            //处理密码保存，如编辑界面未填写密码，则不进行密码变更，否则变更密码以及密码更新时间
            if (string.IsNullOrEmpty(newModel.PASSWORD))
            {
                newModel.PASSWORD = baseService().FindAll(x => x.ID == newModel.ID, u => new B2B_USER { PASSWORD = u.PASSWORD }).Result[0].PASSWORD;
            }
            else
            {
                newModel.PASSWORD_UPDATE_TIME = DateTime.Now;
            }
            return base.Modify(newModel, context);
        }

        /// <summary>
        /// 解除微信绑定
        /// </summary>
        /// <param name="invIoExtend"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<AjaxResult> UnBindWx(UserUnBindWxEntity userUnBindWxEntity, ServerCallContext context)
        {
            AjaxResult result = new AjaxResult();
            return Task.Run<AjaxResult>(() =>
            {
                if (_userService().UnBindWx(userUnBindWxEntity))
                {
                    result.Result = DoResult.Success;
                    result.NumberMsg = 3109;//解绑微信成功
                }
                else
                {
                    result.Result = DoResult.Failed;
                    result.NumberMsg = 3110;//解绑微信失败
                }
                return result;
            });
        }

        public Task<AjaxResult> UpdatePwd(UserUpdatePwdEntity userUpdatePwdEntity, ServerCallContext context)
        { 
            return Task.Run<AjaxResult>(() =>
            {
                //校验老密码是否输入正确
                var userList= this._userService().FindAll(u => u.ID == userUpdatePwdEntity.Id && u.PASSWORD == userUpdatePwdEntity.OldPwd).Result;
                if (userList .Count> 0)
                {
                    userList[0].PASSWORD = userUpdatePwdEntity.NewPwd;

                    if (this._userService().Update(userList[0]).Result)
                    {
                        //密码修改成功
                        return new AjaxResult(DoResult.Success, 3155); 
                    }
                    else
                    {
                        //密码修改失败
                        return new AjaxResult(DoResult.Failed, 3156);
                    }

                }
                else
                {
                    return new AjaxResult(DoResult.Failed, 3154);
                } 
            });
        }
    }
}

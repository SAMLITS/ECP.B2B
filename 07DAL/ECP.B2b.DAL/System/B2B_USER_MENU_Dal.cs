using ECP.B2b.DAL.Interface.System;
using ECP.B2b.DbModel.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ECP.B2b.ComEntity;
using ECP.B2b.DbModel.Basic;
using System.Linq;

namespace ECP.B2b.DAL.System
{
    public class B2B_USER_MENU_Dal : BaseDal<B2B_USER_MENU>, IB2B_USER_MENU_Dal
    {
        public B2B_USER_MENU_Dal(DbContext hbContext) : base(hbContext)
        {
        }

        /// <summary>
        /// 用户菜单分配   删除现有的，插入新的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetMenuByUser(List<B2B_USER_MENU> request)
        {
            using (var tran = _Context.Database.BeginTransaction())
            {
                try
                {
                    List<B2B_USER_MENU> oldUserMenu = this.FindAll(m => m.USER_ID == request[0].USER_ID);
                    this.TDbSet.RemoveRange(oldUserMenu);
                    Context.SaveChanges();
                    this.TDbSet.AddRange(request);
                    Context.SaveChanges();
                    //将已经存在的“默认分配”取值为Y的关联“功能行”，写入当前用户的“功能分配”记录之中
                    var menuFunctionTDbSet = Context.Set<B2B_MENU_FUNCTION>();
                    var defaultAssignMenuFunctions = menuFunctionTDbSet.Where(m => m.DEL_FLAG == 0 && request.Select(u => u.SUBMENU_ID).Contains(m.MENU_ID) &&m.DEFAULT_ASSIGN == "Y").ToList();
                        var userFunctions = new List<B2B_USER_FUNCTION>();
                    defaultAssignMenuFunctions.ForEach(d =>
                    {
                        userFunctions.Add(new B2B_USER_FUNCTION
                        {
                            USER_ID = request[0].USER_ID,
                            MENU_ID = d.MENU_ID,
                            MENU_FUNCTION_ID = d.ID,
                            MENU_FUNCTION_CODE = d.FUNCTION_CODE,
                            CREATION_DATE = request[0].CREATION_DATE,
                            CREATOR = request[0].CREATOR,
                            EDITOR = request[0].EDITOR,
                            LAST_UPDATE_DATE = request[0].LAST_UPDATE_DATE,
                            DEL_FLAG = request[0].DEL_FLAG
                        });
                    });

                    if (userFunctions.Count() > 0)
                    {
                        var userFunctionTDbSet = Context.Set<B2B_USER_FUNCTION>();
                        userFunctionTDbSet.AddRange(userFunctions);
                        Context.SaveChanges();
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
    }
}

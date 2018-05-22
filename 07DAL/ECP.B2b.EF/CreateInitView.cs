using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.B2b.EF
{
    /// <summary>
    /// 创建初始化视图
    /// </summary>
    public class CreateInitView
    {
        public readonly DbContext context;
        public CreateInitView(DbContext _context)
        {
            this.context = _context;
        }

        public  void RunInitCreate(ViewEntity  v)
        {
            try
            {
                string drop = $@" IF EXISTS(Select 1 From Sysobjects Where Name = '{v.viewName}' And XType='V')
                            DROP VIEW [dbo].[{ v.viewName}] ";
                context.Database.ExecuteSqlCommand(drop);

                var createView = $@"CREATE VIEW [dbo].[{v.viewName}] 
                        AS {v.viewScript} ";
                int res = context.Database.ExecuteSqlCommand(createView); 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    /// <summary>
    /// 视图对象
    /// </summary>
    public class ViewEntity
    {
        public ViewEntity(string _viewName, string _viewScript)
        {
            this.viewName = _viewName;
            this.viewScript = _viewScript;
        }

        public string viewName { get; set; }
        public string viewScript { get; set; }
    }
}

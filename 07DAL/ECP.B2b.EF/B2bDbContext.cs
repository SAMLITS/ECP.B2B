using ECP.B2b.DbModel.Basic; 
using ECP.B2b.DbModel.Sys; 
using Microsoft.EntityFrameworkCore;
using System; 

namespace ECP.B2b.EF
{
    public class B2bDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public B2bDbContext(DbContextOptions<B2bDbContext> options) : base(options)
        {
        }
        //菜单
        public DbSet<B2B_MENU> B2B_MENU { get; set; }

        //码表
        public DbSet<B2B_LOOKUP_VALUES_ALL> B2B_LOOKUP_VALUES_ALL { get; set; }

        //码表明细
        public DbSet<B2B_LOOKUP_VALUES> B2B_LOOKUP_VALUES { get; set; }

        //消息
        public DbSet<B2B_MESSAGES> B2B_MESSAGES { get; set; } 
        
        //用户
        public DbSet<B2B_USER> B2B_USER { get; set; }

        public DbSet<B2B_USER_MENU> B2B_USER_MENU { get; set; }
         

        /// <summary>
        ///  菜单功能
        /// </summary>
        public DbSet<B2B_MENU_FUNCTION> B2B_MENU_FUNCTION { get; set; }

        /// <summary>
        ///  用户功能分配
        /// </summary>
        public DbSet<B2B_USER_FUNCTION> B2B_USER_FUNCTION { get; set; }
         

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<B2B_MENU>().HasKey(d => d.MENU_ID);
            base.OnModelCreating(modelBuilder);
        }
    }
}

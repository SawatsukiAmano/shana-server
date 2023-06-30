using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public class EFPostgreSqlContext : DbContext
    {

        public EFPostgreSqlContext() : base()
        {
            //date time 兼容 pgsql
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            Database.EnsureCreated();
        }
        public EFPostgreSqlContext(bool delete) : base()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            if (delete) Database.EnsureDeleted();//清空数据库
            Database.EnsureCreated();
        }

        #region 模型
        public DbSet<SysUser> SysUser { get; set; }

        public DbSet<SysRole> SysRole { get; set; }

        public DbSet<SysUserLoginHistroy> SysUserLoginHistroy { get; set; }

        public DbSet<AdmDepartment> AdmDepartment { get; set; }

        public DbSet<AdmEmployee> AdmEmployee { get; set; }

        public DbSet<SysNode> SysNode { get; set; }

        public DbSet<SysRoleNode> SysRoleNode { get; set; }

        public DbSet<SysRoleUser> sysRoleUser { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 设置蛇形命名
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).ToTable(CommonHelper.CV.PascalUp2Snake(entityType.Name.ToString().Split(".").LastOrDefault()));
                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(CommonHelper.CV.PascalUp2Snake(property.Name));
                }
            }
            #endregion
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=192.168.1.11:40002;Database=shana;Username=postgres;Password=Postgres@2123");
    }
}

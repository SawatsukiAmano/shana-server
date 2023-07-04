using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel;


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

        public DbSet<Signin> SysUserLoginHistroy { get; set; }

        public DbSet<AdmDepartment> AdmDepartment { get; set; }

        public DbSet<AdmEmployee> AdmEmployee { get; set; }

        public DbSet<SysNode> SysNode { get; set; }

        public DbSet<SysRoleNode> SysRoleNode { get; set; }

        public DbSet<SysRoleUser> SysRoleUser { get; set; }

        public DbSet<SysUserLoginRecord> SysUserLoginRecord { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 设置映射
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //蛇形命名表
                modelBuilder.Entity(entityType.Name).ToTable(CommonHelper.CV.PascalUp2Snake(entityType.Name.ToString().Split(".").LastOrDefault()));

                foreach (var property in entityType.GetProperties())
                {
                    //蛇形命名字段
                    property.SetColumnName(CommonHelper.CV.PascalUp2Snake(property.Name));
                    //备注
                    var remarksField = string.Empty;
                    var propertyInfoList = property.PropertyInfo.CustomAttributes;
                    foreach (var item in propertyInfoList)
                    {
                        if (item.AttributeType == typeof(DescriptionAttribute)) remarksField = string.Join(",", item.ConstructorArguments.Select(r => r.Value?.ToString()));
                    }
                    if (remarksField.Length > 0) property.SetComment(remarksField);
                }

            }
            #endregion
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=192.168.1.11:40002;Database=shana;Username=postgres;Password=Postgres@2123");
    }
}

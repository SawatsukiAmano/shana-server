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

        public DbSet<SysUserRole> SysUserRole { get; set; }

        public DbSet<SysUserLoginRecord> SysUserLoginRecord { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 设置映射
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.Name).ToTable(CommonHelper.CV.PascalUp2Snake(entityType.ClrType.Name));
                #region 数据库表名注释
                var remarksTable = string.Empty;
                var propertyEntityInfoList = entityType.ClrType.CustomAttributes;
                foreach (var item in propertyEntityInfoList)
                {
                    if (item.AttributeType == typeof(DescriptionAttribute)) remarksTable = string.Join(",", item.ConstructorArguments.Select(r => r.Value?.ToString()));
                }
                if (remarksTable.Length > 0) entityType.SetComment(remarksTable);
                #endregion

                foreach (var property in entityType.GetProperties())
                {
                    //蛇形命名
                    property.SetColumnName(CommonHelper.CV.PascalUp2Snake(property.Name));
                    #region 数据库字段注释
                    var remarksField = string.Empty;
                    var propertyInfoList = property.PropertyInfo.CustomAttributes;
                    foreach (var item in propertyInfoList)
                    {
                        if (item.AttributeType == typeof(DescriptionAttribute)) remarksField = string.Join(",", item.ConstructorArguments.Select(r => r.Value?.ToString()));
                    }
                    if (remarksField.Length > 0) property.SetComment(remarksField);
                    #endregion
                }

            }
            #endregion
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Host=192.168.1.11:40002;Database=shana;Username=postgres;Password=Postgres@2123");
    }
}

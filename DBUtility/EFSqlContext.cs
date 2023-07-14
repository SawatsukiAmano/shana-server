using CommonHelper;
using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel;
namespace DBUtility
{
    public class EFSqlContext : DbContext
    {

        public EFSqlContext() : base()
        {
            string appEngine = Appsettings.ReadNode("ConnectionStrings:PgSql");
            if (!string.IsNullOrWhiteSpace(appEngine)) sqlEngine = appEngine;

            string tomlEngine = TomlSettings.ReadNode("ConnectionStrings:PgSql");
            if (!string.IsNullOrWhiteSpace(appEngine)) sqlEngine = tomlEngine;
            switch (sqlEngine)
            {
                case "PgSql":
                    AppContext.SetSwitch("NPgSql.EnableLegacyTimestampBehavior", true);
                    AppContext.SetSwitch("NPgSql.DisableDateTimeInfinityConversions", true);
                    break;
                default:
                    break;
            }
            //date time 兼容 PgSql

            Database.EnsureCreated();
        }
        public EFSqlContext(bool delete) : base()
        {
            string appEngine = Appsettings.ReadNode("ConnectionStrings:PgSql");
            if (!string.IsNullOrWhiteSpace(appEngine)) sqlEngine = appEngine;

            string tomlEngine = TomlSettings.ReadNode("ConnectionStrings:PgSql");
            if (!string.IsNullOrWhiteSpace(appEngine)) sqlEngine = tomlEngine;
            switch (sqlEngine)
            {
                case "PgSql":
                    AppContext.SetSwitch("NPgSql.EnableLegacyTimestampBehavior", true);
                    AppContext.SetSwitch("NPgSql.DisableDateTimeInfinityConversions", true);
                    break;
                default:
                    break;
            }
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
                modelBuilder.Entity(entityType.Name).ToTable(CommonHelper.CV.Pascal2Snake(entityType.ClrType.Name));
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
                    property.SetColumnName(CommonHelper.CV.Pascal2Snake(property.Name));
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

        private string sqlEngine = "MsSql";

        private string ConnectString => GetConnectString();

        private string GetConnectString()
        {
            string str = Appsettings.ReadNode($"ConnectionStrings:{sqlEngine}");
            if (!string.IsNullOrEmpty(str)) return str;
            return TomlSettings.ReadNode($"ConnectionStrings:{sqlEngine}");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string str = ConnectString;
            switch (sqlEngine)
            {
                case "PgSql":
                    optionsBuilder.UseNpgsql(str);
                    break;
                case "MsSql":
                    optionsBuilder.UseSqlServer(str);
                    break;
                default:
                    break;
            }


        }

    }
}

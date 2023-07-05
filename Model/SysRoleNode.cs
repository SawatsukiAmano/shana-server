namespace Model
{
    [Description("权限——功能 n-m")]
    public class SysRoleNode
    {
        [Key]
        [Description("主键")]
        public long RnID { get; set; }

        [MaxLength(50)]
        [Description("权限id，一个权限对应多个功能模块")]
        public string RoleID { get; set; }

        [MaxLength(50)]
        [Description("模块id")]
        public string NodeID { get; set; }
    }
}

namespace Model
{
    [Description("角色表")]
    public class SysRole
    {
        [Key]
        [MaxLength(50)]
        [Description("主键")]
        public string RoleID { get; set; }

        [MaxLength(255)]
        [Description("权限描述")]
        public string? Description { get; set; }
    }
}

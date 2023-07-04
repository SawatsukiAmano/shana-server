namespace Model
{
    [Description("用户登录记录")]
    public class SysUserLoginRecord
    {
        [Key]
        public int SulcID { get; set; }

        [MaxLength(50)]
        public string UserID { get; set; }

        [MaxLength(50)]
        public string IPV4 { get; set; }

        [MaxLength(128)]
        public string? IPV6 { get; set; }

        [Description("登录是否成功")]
        public bool Success { get; set; }
    }
}

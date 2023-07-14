namespace Model
{
    [Description("用户表")]
    public class SysUser
    {
        [Key]
        [MaxLength(50)]
        [Description("主键")]
        public string? UserID { get; set; }

        [MaxLength(50)]
        [Description("用户名")]
        public string? UserName { get; set; }

        [MaxLength(50)]
        [Description("密码")]
        public string Password { get; set; }

        [MaxLength(50)]
        [Description("昵称")]
        public string? NickName { get; set; }

        [MaxLength(50)]
        public string? EmployeeID { get; set; }

        [Description("创建时间")]
        public DateTimeOffset CreateAt { get; set; }

        //[NotMapped]
        //public string BlogCode
        //{
        //    get
        //    {
        //        return UserName.Substring(0, 1) + ":" + UserID.Substring(0, 1);
        //    }
        //}
    }


}

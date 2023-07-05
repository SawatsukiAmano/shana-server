namespace Model
{
    [Description("注册记录 1-n")]
    public class Signin
    {
        [Key]
        public string SigninID { get; set; }

        [MaxLength(50)]
        [Description("用户id")]
        public string UserID { get; set; }

        [Description("注册时间")]
        public DateTime CreateAt { get; set; }

        [Description("注册ip")]
        [MaxLength(50)]
        public string IPV4 { get; set; }

        [Description("注册ip")]
        [MaxLength(128)]
        public string? IPV6 { get; set; }

        [Description("注册时http请求信息")]
        public string Headers { get; set; }

        [Description("注册是否成功")]
        public bool Success { get; set; }
    }
}

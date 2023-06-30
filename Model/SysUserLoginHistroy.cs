using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SysUserLoginHistroy
    {
        [Key]
        public string UlhID { get; set; }

        [MaxLength(50)]
        [Description("用户表id")]
        public string UserID { get; set; }

        [Description("登录时间")]
        public DateTime LoginAt { get; set; }

        [Description("登录ip")]
        public string IPAddress { get; set; }
    }
}

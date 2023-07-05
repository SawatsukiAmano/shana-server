using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Model
{
    [Description("用户——权限 n-m")]
    public class SysUserRole
    {
        [Key]
        [Description("主键")]
        public long UrID { get; set; }

        [MaxLength(50)]
        [Description("用户id,一个用户对应多个权限")]
        public string UserID { get; set; }

        [MaxLength(50)]
        [Description("权限表id")]
        public string RoleID { get; set; }
    }
}

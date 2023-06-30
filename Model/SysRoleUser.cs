using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SysRoleUser
    {
        [Key]
        [Description("主键")]
        public long RuID { get; set; }

        [MaxLength(50)]
        [Description("用户id,一个用户对应多个权限")]
        public string UserID { get; set; }

        [MaxLength(50)]
        [Description("权限表id")]
        public string RoleID { get; set; }
    }
}

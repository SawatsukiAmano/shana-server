using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model
{
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

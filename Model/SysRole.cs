using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 角色表
    /// </summary>
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

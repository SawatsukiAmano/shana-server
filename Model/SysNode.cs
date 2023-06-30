using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 系统功能表
    /// </summary>
    public class SysNode
    {
        [Key]
        [MaxLength(50)]
        [Description("主键")]
        public string NodeID { get; set; }

        [Description("节点层级，从1开始")]
        public int NodeHierarchy { get; set; }

        [MaxLength(50)]
        [Description("模块/功能节点名称")]
        public string NodeName { get; set; }

        [MaxLength(255)]
        [Description("节点url，如果需要的话")]
        public string? Url { get; set; }

        [Description("显示排序")]
        public int NodeCode { get; set; }

        [MaxLength(50)]
        [Description("父节点id")]
        public string? ParentID { get; set; }
    }
}

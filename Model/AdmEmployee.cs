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
    /// 员工表
    /// </summary>
    public class AdmEmployee
    {
        [Key]
        [MaxLength(50)]
        [Description("主键")]
        public string EmployeeID { get; set; }

        [MaxLength(50)]
        [Description("员工姓名")]
        public string EmployeeName { get; set; }

        [MaxLength(50)]
        [Description("部门id")]
        public string DepartmentID { get; set; }

        [MaxLength(50)]
        [Description("职位")]
        public string Title { get; set; }

        [Description("行政级别")]
        public int Level { get; set; }
    }
}

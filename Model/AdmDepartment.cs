using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Model
{
    public class AdmDepartment
    {
        [Key]
        [MaxLength(50)]
        public string DepartmentID { get; set; }

        [MaxLength(50)]
        public string DepartmentName { get;set; }
    }
}

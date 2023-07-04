namespace Model
{
    [Description("员工表")]
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

        [MaxLength(50)]
        [Description("行政级别")]
        public int Level { get; set; }
    }
}

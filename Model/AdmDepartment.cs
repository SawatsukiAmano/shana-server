namespace Model
{
    [Description("部门")]
    public class AdmDepartment
    {
        [Key]
        [MaxLength(50)]
        public string DepartmentID { get; set; }

        [MaxLength(50)]
        public string DepartmentName { get; set; }
    }
}

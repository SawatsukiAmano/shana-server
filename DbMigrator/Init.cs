using DBUtility;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMigrator
{
    public static class Init
    {
        public static bool InitDB()
        {

            using (var db = new EFPostgreSqlContext(true))
            {
                AdmDepartment admDepartment = new AdmDepartment()
                {
                    DepartmentID = Guid.NewGuid().ToString(),
                    DepartmentName = "管理员组"
                };
                db.Add(admDepartment);

                AdmEmployee admEmployee = new AdmEmployee()
                {
                    EmployeeID = Guid.NewGuid().ToString(),
                    EmployeeName = "管理员",
                    DepartmentID = admDepartment.DepartmentID,
                    Title = "超级管理员",
                    Level = 1,
                };
                db.Add(admEmployee);

                var user = new SysUser()
                {
                    UserID = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    Password = CommonHelper.CV.ComputeMD5("admin"),
                    CreateAt = DateTime.Now,
                    NickName = "admin",
                    EmployeeID = admEmployee.EmployeeID,
                };
                db.Add(user);


                SysRole sysRole = new SysRole()
                {
                    RoleID = Guid.NewGuid().ToString(),
                    Description = "管理员权限"
                };
                db.Add(sysRole);

                SysUserRole sysUserRole = new SysUserRole()
                {
                    RoleID = sysRole.RoleID,
                    UserID = user.UserID,
                };
                db.Add(sysUserRole);

                SysNode sysNode = new SysNode()
                {
                    NodeID = Guid.NewGuid().ToString(),
                    NodeHierarchy = 1,
                    NodeName = "系统设置",
                    NodeCode = 1,
                };
                db.Add(sysNode);

                SysRoleNode sysRoleNode = new SysRoleNode()
                {
                    RoleID = sysRole.RoleID,
                    NodeID = sysNode.NodeID,
                };
                db.Add(sysRoleNode);

                return db.SaveChanges() > 0;
            }
        }

    }
}

using DAL.Base;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALSysUser : BaseDAL<SysUser>, IDALSysUser
    {
        public virtual string GetDAL() => "mainDAL";

    }
}

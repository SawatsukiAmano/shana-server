using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLSysUserA1:BLLSysUser
    {
        public BLLSysUserA1(IEnumerable<IDALSysUser> dal) : base(dal)
        {
        }

        public override string GetStr() => "sub";

        public override string GetBll() => "subBLLA1" + ":" + _dalSysUser.GetDAL();
    }
}

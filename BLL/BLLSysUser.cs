using IDAL;
using IDAL.Base;

namespace BLL
{
    public class BLLSysUser : BaseBLL<SysUser>, IBLLSysUser
    {
        protected readonly IDALSysUser _dalSysUser;
        public BLLSysUser() { 
        }

        public BLLSysUser(IDALSysUser dal)
        {
            this._dalSysUser = dal;
            base._baseDal = dal;
        }

        public virtual string GetStr() => "main";

        public virtual string GetDAL() => _dalSysUser.GetDAL();

        public virtual string GetBll()
        {
            return "mainBLL"+":"+_dalSysUser.GetDAL();
        }
    }
}

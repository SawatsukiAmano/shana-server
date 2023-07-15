using IDAL;
using IDAL.Base;

namespace BLL
{
    public class BLLSysUser : BaseBLL<SysUser>, IBLLSysUser
    {
        protected readonly IDALSysUser _dalSysUser;

        public BLLSysUser(IEnumerable<IDALSysUser> dal)
        {
            var dalName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name.Replace("BLL", "DAL");
            _dalSysUser = dal.First(x => x.GetType().Name == dalName);
            _baseDal = _dalSysUser;
        }

        public virtual string GetStr() => "main";

        public virtual string GetDAL() => _dalSysUser.GetDAL();

        public virtual string GetBll()
        {
            return "mainBLL" + ":" + _dalSysUser.GetDAL();
        }
    }
}

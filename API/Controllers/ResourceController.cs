
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{

    [Description("资源控制器")]
    public class ResourceController 
    {
        private readonly string _path = Appsettings.ReadNode("SysSetting:ResourcePath");

        [Description("获取所有路径")]
        //[HttpGet]
        public string ListResource()
        {
            return string.Join(",",Fileopera.GetDirectorySubFileName(_path));
        }

    }
}

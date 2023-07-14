
using AutoMapper;
using IBLL;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{

    [Description("资源控制器")]
    public class ResourceController : BaseController
    {
        private string _path => StaticValue.ResourcePath;
        private readonly IBLLSysUser _user;
        public ResourceController(IBLLSysUser user,IMapper mapper, ILogger<ResourceController> logger, IHttpContextAccessor httpContextAccessor) : base(mapper,logger, httpContextAccessor)
        {
            _user = user;
        }

        [Description("获取所有路径")]
        [HttpGet]
        public string ListResource()
        {
            return string.Join(",", Fileopera.GetDirectorySubFileName(_path));
        }


        [HttpGet]
        public string TT()
        {
            _logger.LogDebug("调试日志");
            _logger.LogInformation("信息日志");
            _logger.LogWarning("警告日志");
            _logger.LogCritical("严重日志");
            _logger.LogError("错误日志");
            return new BLL.BLLSysUser().GetStr()+":"+ new BLL.BLLSysUserA1().GetStr();
        }

        [HttpGet]
        public string TT2()
        {
            _logger.LogDebug("调试日志");
            _logger.LogInformation("信息日志");
            _logger.LogWarning("警告日志");
            _logger.LogCritical("严重日志");
            _logger.LogError("错误日志");
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
            return _user.GetBll();
        }

    }
}

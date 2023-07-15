
using AutoMapper;
using CommonHelper;
using IBLL;
using log4net;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Controllers
{

    [Description("资源控制器")]
    public class ResourceController : BaseController
    {
        private readonly ILog _log4;
        private readonly IBLLSysUser _user;
        public ResourceController(IEnumerable<IBLLSysUser> users, IMapper mapper, ILogger<ResourceController> logger, IHttpContextAccessor httpContextAccessor) : base(mapper, logger,  httpContextAccessor)
        {
            _user = users.First(s=>s.GetType().Name== "BLLSysUser");
            _log4 = new Log<ResourceController>()._log4;
        }
        private string _path => StaticValue.ResourcePath;


        /// <summary>
        /// 获取所有路径
        /// </summary>
        /// <returns></returns>
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
            return "tt" + DateTime.Now.ToString();
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

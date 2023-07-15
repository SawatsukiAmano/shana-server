


using log4net;

namespace API.Base
{

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    //[CustomOrdersRoute]
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly ILogger _logger;

        public BaseController(IMapper mapper, ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _contextAccessor = httpContextAccessor;
            _logger = logger;
        }

    }
}

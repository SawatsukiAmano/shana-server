

namespace API.Base
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


    }
}

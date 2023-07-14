using DBUtility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        IMapper _mapper;

        public HomeController(IMapper mapper, ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public string TT() => "33";
    }
}

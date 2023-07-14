


namespace API.Base
{

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    //[CustomOrdersRoute]
    public class BaseController : Controller
    {
        private readonly IMapper _mapper;


    }
}

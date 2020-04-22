using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class SkinetBaseController : ControllerBase
    {

        private IMapper _mapper;
        // MUST USE using Microsoft.Extensions.DependencyInjection;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}
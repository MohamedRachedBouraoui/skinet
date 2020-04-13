using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    // So swagger don't try to document this controller, otherwise 
    //it will throw an Exception because we are not specifying any Http verb here because we want this
    // Controller to handle all Request types
    public class ErrorController : SkinetBaseController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }

    }
}
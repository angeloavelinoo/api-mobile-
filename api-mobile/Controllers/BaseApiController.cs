using api_mobile.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        public IActionResult ServiceResponse<T>(ResultModel<T> response)
        {

            return StatusCode(response.Status, response);
        }
    }
}
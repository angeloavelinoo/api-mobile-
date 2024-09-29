using api_mobile.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class ValidadeController(ValidadeService validadeService) : BaseApiController
    {
        private readonly ValidadeService _validadeService = validadeService;
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int produtoId)
        {
            return ServiceResponse(await _validadeService.GetAll(produtoId));
        }
    }
}

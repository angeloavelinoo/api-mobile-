using api_mobile.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class MovimentacaoController(MovimentacaoService movimentacaoService) : BaseApiController 
    {
        private readonly MovimentacaoService _movimentacaoService = movimentacaoService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string jwt = HttpContext.Request.Headers["Authorization"];

            int idUsuario = TokenService.GetUserIdFromToken(jwt);

            return ServiceResponse(await _movimentacaoService.GetAll(idUsuario));
        }
    }
}

using api_mobile.DTOs.EstoquesDTO;
using api_mobile.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class EstoqueController(EstoqueService estoqueService) : BaseApiController
    {
        private readonly EstoqueService _estoqueService = estoqueService;

        [HttpPost]
        public async Task<IActionResult> Add(EstoqueCreateDTO estoqueDTO)
        {
            string jwt = HttpContext.Request.Headers["Authorization"];

            int idUsuario = TokenService.GetUserIdFromToken(jwt);

            return ServiceResponse(await _estoqueService.Add(estoqueDTO, idUsuario));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string jwt = HttpContext.Request.Headers["Authorization"];

            int idUsuario = TokenService.GetUserIdFromToken(jwt);

            return ServiceResponse(await _estoqueService.GetAll(idUsuario));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ServiceResponse(await _estoqueService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EstoqueCreateDTO estoqueDTO, int id)
        {
            return ServiceResponse(await _estoqueService.Update(id ,estoqueDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return ServiceResponse(await _estoqueService.Delete(id));
        }
    }
}

using api_mobile.DTOs.UsuariosDTO;
using api_mobile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class UsuarioController(UsuarioService usuarioService, IHttpContextAccessor httpContextAccessor) : BaseApiController
    {
        private readonly UsuarioService _usuarioService = usuarioService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(RegisterDTO usuario)
        {
            return ServiceResponse(await _usuarioService.Add(usuario));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return ServiceResponse(await _usuarioService.GetById(id));
        }
    }
}
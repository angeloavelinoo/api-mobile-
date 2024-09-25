using api_mobile.DTOs.UsuariosDTO;
using api_mobile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_mobile.Controllers
{
    public class LoginController(UsuarioService usuarioService) : BaseApiController
    {
        private readonly UsuarioService _usuarioService = usuarioService;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO usuarioLoginDTO)
        {
            return ServiceResponse(await _usuarioService.Login(usuarioLoginDTO));
        }
    }
}
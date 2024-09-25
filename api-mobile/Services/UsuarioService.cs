using api_mobile.DTOs.UsuariosDTO;
using api_mobile.Model;
using api_mobile.Repository;
using api_mobile.ViewModel;
using System.Net;

namespace api_mobile.Services
{
    public class UsuarioService(UsuarioRepository usuarioRepository)
    {
        private readonly UsuarioRepository _usuarioRepository = usuarioRepository;
        public async Task<ResultModel<dynamic>> Add(RegisterDTO usuarioDTO)
        {
            var usuario = new Usuario(
                new(usuarioDTO.Nome), (usuarioDTO.Email),
                 BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha), (usuarioDTO.Role));

            if (await _usuarioRepository.AlredyExist(usuarioDTO.Email))
                return new(HttpStatusCode.Conflict, "Email já cadastrado");

            await _usuarioRepository.Create(usuario);

            return new();

        }

        public async Task<ResultModel<dynamic>> Login(LoginDTO usuarioDTO)
        {
            Usuario usuario = await _usuarioRepository.GetByEmail(usuarioDTO.Email);

            if (usuario?.Senha != null && BCrypt.Net.BCrypt.Verify(usuarioDTO.Senha, usuario.Senha))
                return new ResultModel<dynamic>(new
                {
                    token = TokenService.GenerateToken(usuario)
                });

            return new(HttpStatusCode.BadRequest, "Email ou senha inválida");
        }

        public async Task<ResultModel<UsuarioDTO>> GetById(int id)
        {
            Usuario usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return new(HttpStatusCode.NotFound, "Usuario não encontrado");

            UsuarioDTO usuarioDTO = new()
            {
                Nome = usuario.Nome,
            };

            return new (usuarioDTO);
        }
    }
}
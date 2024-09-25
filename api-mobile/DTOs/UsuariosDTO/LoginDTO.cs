using api_mobile.Contracts;

namespace api_mobile.DTOs.UsuariosDTO
{
    public class LoginDTO : BaseDTOValidation
    {
        public LoginDTO(string email, string senha)
        {
            AddNotifications(new ContractEmail(email),
                new ContractPassword(senha));
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }
        public string Senha { get; set; }
    }
}

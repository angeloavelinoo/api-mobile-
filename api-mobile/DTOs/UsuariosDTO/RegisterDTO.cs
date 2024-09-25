using api_mobile.Contracts;

namespace api_mobile.DTOs.UsuariosDTO
{
    public class RegisterDTO : BaseDTOValidation
    {
        public RegisterDTO(string nome, string email, string senha, string? role)
        {
            AddNotifications(new ContractEmail(email),
                new ContractPassword(senha),
                new ContractName(nome));

            Nome = nome;
            Email = email;
            Senha = senha;
            Role = role;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Role { get; set; }


    }
}
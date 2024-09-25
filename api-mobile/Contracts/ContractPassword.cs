using Flunt.Validations;

namespace api_mobile.Contracts
{
    public class ContractPassword : Contract<string>
    {
        public ContractPassword(string password = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(password, nameof(password), "Senha inválida");
        }
    }
}
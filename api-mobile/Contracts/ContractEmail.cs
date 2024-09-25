using Flunt.Validations;

namespace api_mobile.Contracts
{
    public class ContractEmail : Contract<string>
    {
        public ContractEmail(string emailAddress = "")
        {
            Requires()
                .IsEmail(emailAddress, nameof(emailAddress), "Email inválido");
        }
    }
}
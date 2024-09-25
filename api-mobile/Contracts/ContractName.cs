using Flunt.Validations;

namespace api_mobile.Contracts
{
    public class ContractName : Contract<string>
    {
        public ContractName(string name = "")
        {
            Requires()
                .IsNotNullOrWhiteSpace(name, nameof(name), "Nome é requirido")
                    .IsGreaterThan(name?.Length ?? 0, 3, nameof(name), "Nome deve ter ao menos 3 caracteres");
        }
    }
}
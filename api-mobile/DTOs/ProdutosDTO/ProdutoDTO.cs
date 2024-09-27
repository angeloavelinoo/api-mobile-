using api_mobile.Model;

namespace api_mobile.DTOs.ProdutosDTO
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Nome { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeMinima { get; set; }
        public decimal Valor { get; set; }
        public int EstoqueId { get; set; }
        public string CategoriaNome { get; set; }
        public List<ValidadeDTO> Validades { get; set; }
    }
}

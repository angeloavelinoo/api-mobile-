namespace api_mobile.DTOs.ProdutosDTO
{
    public class ProdutoCreateDTO
    {
        public required string CodigoBarras { get; set; }
        public required string Nome { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeMinima { get; set; }
        public decimal Valor { get; set; }
        public int EstoqueId { get; set; }
        public int CategoriaId { get; set; }
        public DateOnly DataValidade { get; set; }
    }
}

namespace api_mobile.DTOs
{
    public class CompraDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateOnly Validade { get; set; }
    }
}

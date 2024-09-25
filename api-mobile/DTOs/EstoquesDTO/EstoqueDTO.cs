namespace api_mobile.DTOs.EstoquesDTO
{
    public class EstoqueDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<ProdutosDTO.ProdutoDTO>? Produtos { get; set; }
    }
}

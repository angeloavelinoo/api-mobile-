namespace api_mobile.Model
{
    public class Validade
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public DateOnly DataValidade { get; set; }
        public Produto Produto { get; set; }

        public Validade(int produtoId,DateOnly dataValidade)
        {
            ProdutoId = produtoId;
            DataValidade = dataValidade;
        }
    
    }
}

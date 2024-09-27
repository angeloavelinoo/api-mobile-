namespace api_mobile.Model
{
    public class Validade
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public DateOnly DataValidade { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public Validade(int produtoId,DateOnly dataValidade, int quantidade)
        {
            ProdutoId = produtoId;
            DataValidade = dataValidade;
            Quantidade = quantidade;
        }
    
    }
}

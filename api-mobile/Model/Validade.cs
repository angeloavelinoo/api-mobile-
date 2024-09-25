namespace api_mobile.Model
{
    public class Validade
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public DateTime DataValidade { get; set; }
    }
}

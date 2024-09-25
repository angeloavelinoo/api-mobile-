namespace api_mobile.Model
{
    public class ListaCompra
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataGeracao { get; set; }
    }
}

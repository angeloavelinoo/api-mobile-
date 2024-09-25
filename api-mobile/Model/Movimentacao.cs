namespace api_mobile.Model
{
    public class Movimentacao
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // entrada, saida, exclusao
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public decimal ValorTotal { get; set; }
    }
}

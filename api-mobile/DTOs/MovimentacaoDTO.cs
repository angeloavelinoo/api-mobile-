using api_mobile.Model;

namespace api_mobile.DTOs
{
    public class MovimentacaoDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // entrada, saida, exclusao
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public decimal ValorTotal { get; set; }
    }
}

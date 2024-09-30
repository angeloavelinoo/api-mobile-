namespace api_mobile.Model
{
    public class Produto : Entidade
    {
        public Produto(string codigoBarras, string nome, int quantidade, int quantidadeMinima, decimal valor, int estoqueId, bool ativo)
        {
            CodigoBarras = codigoBarras;
            Nome = nome;
            Quantidade = quantidade;
            QuantidadeMinima = quantidadeMinima;
            Valor = valor;
            EstoqueId = estoqueId;
            Ativo = ativo;
        }

        public  string CodigoBarras { get; set; }
        public  string Nome { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadeMinima { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Valor { get; set; }
        public int EstoqueId { get; set; }
        public  Estoque Estoque { get; set; }
        public List<ProdutoCategoria>? ProdutosCategorias { get; set; }
        public List<Movimentacao>? Movimentacoes { get; set; }
        public List<ListaCompra>? ListaCompras { get; set; }
        public List<Validade>? Validades { get; set; }
    }
}

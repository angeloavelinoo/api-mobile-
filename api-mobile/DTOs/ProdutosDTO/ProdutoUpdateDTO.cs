namespace api_mobile.DTOs.ProdutosDTO;

public class ProdutoUpdateDTO
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
    public int QuantidadeMinima { get; set; }
    public string CodigoBarras { get; set; }
    public int EstoqueId { get; set; }
    public int CategoriaId { get; set; }
    public DateOnly DataValidade { get; set; }
}
namespace api_mobile.Model
{
    public class ProdutoCategoria
    {
        public ProdutoCategoria(int produtoId, int categoriaId)
        {
            ProdutoId = produtoId;
            CategoriaId = categoriaId;
        }

        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }
        public Produto Produto { get; set; }
        public Categoria Categoria { get; set; }
    }
}

﻿namespace api_mobile.Model
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<ProdutoCategoria>? ProdutosCategorias { get; set; }
    }
}

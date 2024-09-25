namespace api_mobile.Model
{
    public class Estoque : Entidade
    {
        public Estoque(string nome, int usuarioId, bool ativo)
        {
            Nome = nome;
            UsuarioId = usuarioId;
            Ativo = ativo;
        }

        public string Nome { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}

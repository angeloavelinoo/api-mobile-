using api_mobile.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace api_mobile.Data

{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ProdutoCategoria> ProdutosCategorias { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<ListaCompra> ListaCompras { get; set; }
        public DbSet<Validade> Validades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class ProdutoCategoriaMap : IEntityTypeConfiguration<ProdutoCategoria>
    {
        public void Configure(EntityTypeBuilder<ProdutoCategoria> builder)
        {
            builder.HasKey(pc => new { pc.ProdutoId, pc.CategoriaId });

            builder.HasOne(pc => pc.Produto)
                   .WithMany(p => p.ProdutosCategorias)
                   .HasForeignKey(pc => pc.ProdutoId);

            builder.HasOne(pc => pc.Categoria)
                   .WithMany(c => c.ProdutosCategorias)
                   .HasForeignKey(pc => pc.CategoriaId);
        }
    }

}
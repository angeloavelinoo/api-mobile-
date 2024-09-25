using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);

            builder.HasMany(c => c.ProdutosCategorias)
                   .WithOne(pc => pc.Categoria)
                   .HasForeignKey(pc => pc.CategoriaId);
        }
    }
}

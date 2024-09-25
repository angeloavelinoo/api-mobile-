using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class EstoqueMap : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.Usuario)
                   .WithMany(u => u.Estoques)
                   .HasForeignKey(e => e.UsuarioId);

            builder.HasMany(e => e.Produtos)
                   .WithOne(p => p.Estoque)
                   .HasForeignKey(p => p.EstoqueId);

            builder.Property(e => e.Ativo).HasDefaultValue(true);
            builder.Property(e => e.Excluido).HasDefaultValue(false);
        }
    }
}
using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Senha).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Role).HasMaxLength(50);

            builder.HasMany(u => u.Estoques)
                   .WithOne(e => e.Usuario)
                   .HasForeignKey(e => e.UsuarioId);

            builder.Property(u => u.Ativo).HasDefaultValue(true);
            builder.Property(u => u.Excluido).HasDefaultValue(false);
        }
    }
}

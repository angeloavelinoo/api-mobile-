using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class ValidadeMap : IEntityTypeConfiguration<Validade>
    {
        public void Configure(EntityTypeBuilder<Validade> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.DataValidade)
                   .IsRequired();

            builder.HasOne(v => v.Produto)
                   .WithMany(p => p.Validades)
                   .HasForeignKey(v => v.ProdutoId);
        }
    }
}

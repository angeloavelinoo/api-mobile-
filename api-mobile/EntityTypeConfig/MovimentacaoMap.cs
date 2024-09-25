using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class MovimentacaoMap : IEntityTypeConfiguration<Movimentacao>
    {
        public void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Tipo)
                   .IsRequired()
                   .HasMaxLength(50); // Define o tipo de movimentação: entrada, saída, exclusão

            builder.Property(m => m.Quantidade)
                   .IsRequired();

            builder.Property(m => m.DataMovimentacao)
                   .IsRequired();

            builder.Property(m => m.ValorTotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(m => m.Produto)
                   .WithMany(p => p.Movimentacoes)
                   .HasForeignKey(m => m.ProdutoId);
        }
    }
}

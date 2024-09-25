using api_mobile.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api_mobile.EntityTypeConfig
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
            builder.Property(p => p.CodigoBarras).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Quantidade).IsRequired();
            builder.Property(p => p.QuantidadeMinima).IsRequired();
            builder.Property(p => p.DataCadastro).HasDefaultValueSql("NOW()");
            builder.Property(p => p.Valor).HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Estoque)
                   .WithMany(e => e.Produtos)
                   .HasForeignKey(p => p.EstoqueId);

            // Relacionamento Produto-Categoria (many-to-many)
            builder.HasMany(p => p.ProdutosCategorias)
                   .WithOne(pc => pc.Produto)
                   .HasForeignKey(pc => pc.ProdutoId);

            // Relacionamento Produto-Movimentacao
            builder.HasMany(p => p.Movimentacoes)
                   .WithOne(m => m.Produto)
                   .HasForeignKey(m => m.ProdutoId);

            builder.HasMany(p => p.ListaCompras)
                   .WithOne(lc => lc.Produto)
                   .HasForeignKey(lc => lc.ProdutoId);

            builder.HasMany(p => p.Validades)
                   .WithOne(v => v.Produto)
                   .HasForeignKey(v => v.ProdutoId);
        }
    }
}
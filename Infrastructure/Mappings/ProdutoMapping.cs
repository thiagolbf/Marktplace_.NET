namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");
            
        builder.Property(x => x.Preco)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .HasColumnType("datetime2");

        //1:N => Produtor : Produtos
        builder.HasOne(x => x.Vendedor)
            .WithMany(x => x.Produtos)
            .HasForeignKey(x => x.VendedorId)
            .IsRequired();

        builder.ToTable("Produtos");
    }
}

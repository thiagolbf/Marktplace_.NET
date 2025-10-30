namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Preco)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Pedido)
            .WithMany(x => x.PedidoItens)
            .HasForeignKey(x => x.PedidoId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.Produto)
            .WithMany(x => x.PedidoItens)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict) //Produto não pode ser apagado caso tenha pedido
            .IsRequired();

        builder.ToTable("PedidoItens");

    }
}

namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion<int>() //opcional
            .IsRequired();

        builder.Property(x => x.ValorTotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        //1:N => Cliente : Pedidos
        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Pedidos)
            .HasForeignKey(x => x.ClienteId);

        builder.ToTable("Pedidos");
    }
}

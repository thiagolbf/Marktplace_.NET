namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MetodoPagamento)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Total)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.StatusPagamento)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.TransacaoId)
            .IsRequired()
            .HasMaxLength(250)
            .HasColumnType("varchar(250)");
            

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        //1:1 => Pagamento : Pedido
        builder.HasOne(x => x.Pedido)
            .WithOne(x => x.Pagamento)
            .HasForeignKey<Pagamento>(x => x.PedidoId);

        builder.ToTable("Pagamentos");

    }
}

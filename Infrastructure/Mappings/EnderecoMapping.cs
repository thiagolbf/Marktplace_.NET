namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.EnderecoValor, ev =>
        {
            ev.Property(p => p.Rua)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

            ev.Property(p => p.Numero)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

            ev.Property(p => p.Bairro)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

            ev.Property(p => p.Cidade)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

            ev.Property(p => p.Cep)
            .IsRequired()
            .HasMaxLength(9)
            .HasColumnType("varchar(9)");
        });

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        //1:N => Cliente : Enderecos
        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Enderecos)
            .HasForeignKey(x => x.ClienteId)
            .IsRequired();

        builder.ToTable("Enderecos");
    }
}



namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar(150)");

        builder.Property(x => x.CPF)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnType("varchar(11)");

        builder.Property(x => x.Telefone)
            .IsRequired()
            .HasMaxLength(11)
            .HasColumnType("varchar(11)");

        builder.Property(x => x.Ativo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        //1:1 => Cliente : AspNetUsers
        builder.HasOne(x => x.ApplicationUser)
            .WithOne(x => x.Cliente)
            .HasForeignKey<Cliente>(x => x.ApplicationUserId);

        builder.ToTable("Clientes");

    }
}

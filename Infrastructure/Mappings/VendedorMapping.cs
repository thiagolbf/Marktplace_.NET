namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar(150)");

        builder.Property(x => x.CNPJ)
            .IsRequired()
            .HasMaxLength(14)
            .HasColumnType("varchar(14)");

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

        //1:1 => Produtor : AspNetUsers
        builder.HasOne(x => x.ApplicationUser)
            .WithOne(x => x.Vendedor)
            .HasForeignKey<Vendedor>(x => x.ApplicationUserId);

        builder.ToTable("Vendedores");


    }
}

namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Ativo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.ToTable("Categorias");

    }
}

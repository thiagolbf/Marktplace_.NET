namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nota)
            .IsRequired();

        builder.Property(x => x.Comentario)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");

        builder.Property(x => x.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(x => x.AtualizadoEm)
            .IsRequired()
            .HasColumnType("datetime2");

        // N:1 => Avaliacao : Cliente
        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Avaliacoes)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        // N:1 => Avaliacao : Produto
        builder.HasOne(x => x.Produto)
            .WithMany(x => x.Avaliacoes)
            .HasForeignKey(x => x.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Regra para que Cliente só consiga avaliar produto uma vez
        builder.HasIndex(x => new { x.ClienteId, x.ProdutoId })
            .IsUnique();

        // Regra para banco também barrar nota
        builder.ToTable(x => x.HasCheckConstraint("CK_Avaliacao_Nota", "[Nota] >= 1 AND [NOTA] <= 5"));

        builder.ToTable("Avaliacoes");

    }
}

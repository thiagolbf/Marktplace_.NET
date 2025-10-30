namespace Markplace.Infrastructure.Mappings;

using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProdutoCategoriaMapping : IEntityTypeConfiguration<ProdutoCategoria>
{
    public void Configure(EntityTypeBuilder<ProdutoCategoria> builder)
    {

        // Chave composta (ProdutoId, CategoriaId)
        builder.HasKey(x => new { x.ProdutoId, x.CategoriaId });

        // Relacionamento com Produto (N:1)
        builder.HasOne(x => x.Produto)
            .WithMany(x => x.ProdutoCategorias)
            .HasForeignKey(x => x.ProdutoId);

        // Relacionamento com Categoria (N:1)
        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.ProdutoCategorias)
            .HasForeignKey(x => x.CategoriaId);

        builder.ToTable("ProdutoCategorias");
    }
}

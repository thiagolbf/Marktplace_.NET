
using Markplace.Domain.Entities;

namespace Markplace.Application.DTOs.ProdutoContracts;

public static class ProdutoMappings
{
    public static Produto ToEntity (this ProdutoDTO produtodto)
    {
        if (produtodto is null)
        {
            throw new ArgumentNullException(nameof(produtodto));
        }

        return new Produto(
            produtodto.Nome,
            produtodto.Descricao,
            produtodto.Preco,
            produtodto.Quantidade,
            produtodto.VendedorId
            );
    }

    public static ProdutoDTO ToProdutoDTO(this Produto produto)
    {
        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produto));
        }


        return new ProdutoDTO(
            produto.Nome ?? string.Empty,
            produto.Descricao ?? string.Empty,
            produto.Preco,
            produto.Quantidade,
            produto.VendedorId,
            produto.ProdutoCategorias.Select(c => c.CategoriaId).ToList()
        );

    }

    public static ProdutoResponseDTO ToProdutoResponseDTO(this Produto produto)
    {
        if (produto is null)
        {
            throw new ArgumentNullException(nameof(produto));
        }

        return new ProdutoResponseDTO(
            produto.Id,
            produto.Nome ?? string.Empty,
            produto.Descricao ?? string.Empty,
            produto.Preco,
            produto.Quantidade,
            produto.Vendedor?.Empresa ?? string.Empty,
            produto.ProdutoCategorias.Select(c => c.CategoriaId).ToList()
        );
    }

    public static IEnumerable<ProdutoResponseDTO> ToProdutoResponseDTOList(this IEnumerable<Produto> produtos)
    {
        if (produtos is null)
            throw new ArgumentException(nameof(produtos));

        return produtos.Select(p => p.ToProdutoResponseDTO());
    }

    public static IEnumerable<ProdutoDTO> ToProdutoDTOList(this IEnumerable<Produto> produtos)
    {
        if (produtos is null)
            throw new ArgumentException(nameof(produtos));

        return produtos.Select(p => p.ToProdutoDTO());
    }

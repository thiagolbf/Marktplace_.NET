using Markplace.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Markplace.Application.DTOs.Mappings;

public static class ProdutoDTOExtensions
{
    public static Produto? ToProduto(this ProdutoDTO produtoDto)
    {
        //if (produtoDto == null) return null;

        return new Produto(
                produtoDto.Nome,
                produtoDto.Descricao,
                produtoDto.Preco,
                produtoDto.Quantidade,
                produtoDto.VendedorId 
                );

    }

    public static ProdutoDTO? ToCategoriaDTO (this Produto produto)
    {
        if (produto == null) return null;

        return new ProdutoDTO
        {
            Nome = produto.Nome,
            //Descricao = produto.Descricao,
            //Preco = produto.Preco,
            //Quantidade = produto.Quantidade,
            //VendedorId = produto.VendedorId,
            //CRIAR RESPONSE DTO
        };
    }

}

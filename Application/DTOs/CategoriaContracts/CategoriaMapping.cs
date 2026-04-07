using Markplace.Domain.Entities;
using System.Net.NetworkInformation;

namespace Markplace.Application.DTOs.CategoriaContracts;

public static class CategoriaMapping
{
    public static Categoria ToEntity (this CategoriaDTO categoriadto)
    {
        if (categoriadto is null)
            throw new ArgumentNullException(nameof(categoriadto));

        return new Categoria(categoriadto.Nome);
    }

    public static CategoriaDTO ToCategoriaDTO(this Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        return new CategoriaDTO
        {
            Nome = categoria.Nome
        };
    }

    public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        if (categorias is null)
            throw new ArgumentException(nameof(categorias));

        return categorias.Select(c => c.ToCategoriaDTO());
    }

    public static CategoriaDetalheDTO ToCategoriaDetalheDTO(this Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentException(nameof(categoria));

        return new CategoriaDetalheDTO(
            categoria.Nome,
            categoria.CriadoEm,
            categoria.AtualizadoEm
            );
    }
}

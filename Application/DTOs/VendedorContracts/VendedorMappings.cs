using Markplace.Domain.Entities;

namespace Markplace.Application.DTOs.VendedorContracts;

public static class VendedorMappings
{
    public static Vendedor ToEntity(this VendedorCompletarDTO vendedorDto, string applicationUserId)
    {
        if (vendedorDto is null)
            throw new ArgumentNullException(nameof(vendedorDto));

        return new Vendedor(
            vendedorDto.Empresa,
            vendedorDto.Descricao,
            vendedorDto.Cnpj,
            vendedorDto.Telefone,
            applicationUserId
        );
    }

    public static VendedorDTO ToVendedorDTO(this Vendedor vendedor)
    {
        if (vendedor is null)
            throw new ArgumentNullException(nameof(vendedor));

        return new VendedorDTO(
            vendedor.Empresa,
            vendedor.Descricao,
            vendedor.CNPJ,
            vendedor.Telefone
        );
    }

    public static VendedorPerfilDTO ToVendedorPerfilDTO(this Vendedor vendedor)
    {
        if (vendedor is null)
            throw new ArgumentNullException(nameof(vendedor));

        return new VendedorPerfilDTO(
            vendedor.Empresa,
            vendedor.Descricao,
            vendedor.ApplicationUser?.Email,
            vendedor.Telefone,
            vendedor.CNPJ,
            vendedor.Ativo,
            vendedor.CriadoEm
        );
    }
}

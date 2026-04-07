using Markplace.Domain.Entities;
using Markplace.Domain.ValueObjects;

namespace Markplace.Application.DTOs.EnderecoContracts;

public static class EnderecoMappings
{
    public static EnderecoValor ToValueObject(this EnderecoDTO dto)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));

        return new EnderecoValor(dto.Rua, dto.Numero, dto.Bairro, dto.Cidade, dto.Estado, dto.Cep);
    }

    public static EnderecoDetalheDTO ToDetalheDTO(this Endereco endereco)
    {
        if (endereco is null) throw new ArgumentNullException(nameof(endereco));

        return new EnderecoDetalheDTO(
            endereco.Id,
            endereco.EnderecoValor?.Rua,
            endereco.EnderecoValor?.Numero,
            endereco.EnderecoValor?.Bairro,
            endereco.EnderecoValor?.Cidade,
            endereco.EnderecoValor?.Estado,
            endereco.EnderecoValor?.Cep,
            endereco.CriadoEm
        );
    }

    public static IEnumerable<EnderecoDetalheDTO> ToDetalheDTOList(this IEnumerable<Endereco> enderecos)
    {
        return enderecos.Select(e => e.ToDetalheDTO());
    }
}

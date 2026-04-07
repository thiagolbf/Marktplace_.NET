using Markplace.Domain.Entities;

namespace Markplace.Application.DTOs.AvaliacaoContracts;

public static class AvaliacaoMappings
{
    public static Avaliacao ToEntity(this AvaliacaoCriarDTO dto, int clienteId)
    {
        if (dto is null) throw new ArgumentNullException(nameof(dto));
        return new Avaliacao(dto.Nota, dto.Comentario, clienteId, dto.ProdutoId);
    }

    public static AvaliacaoDetalheDTO ToDetalheDTO(this Avaliacao avaliacao)
    {
        if (avaliacao is null) throw new ArgumentNullException(nameof(avaliacao));
        return new AvaliacaoDetalheDTO(
            avaliacao.Id,
            avaliacao.ProdutoId,
            avaliacao.Nota,
            avaliacao.Comentario,
            avaliacao.CriadoEm,
            avaliacao.AtualizadoEm
        );
    }

    public static IEnumerable<AvaliacaoDetalheDTO> ToDetalheDTOList(this IEnumerable<Avaliacao> avaliacoes)
    {
        return avaliacoes.Select(a => a.ToDetalheDTO());
    }
}

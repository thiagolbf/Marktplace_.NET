namespace Markplace.Application.DTOs.AvaliacaoContracts;

public record AvaliacaoDetalheDTO
(
    int Id,
    int ProdutoId,
    int Nota,
    string? Comentario,
    DateTime CriadoEm,
    DateTime AtualizadoEm
);

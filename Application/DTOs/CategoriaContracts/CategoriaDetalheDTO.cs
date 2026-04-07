namespace Markplace.Application.DTOs.CategoriaContracts;

public record CategoriaDetalheDTO(
    string Nome,
    DateTime CriadoEm,
    DateTime? AtualizadoEm
);

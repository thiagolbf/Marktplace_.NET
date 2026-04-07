namespace Markplace.Application.DTOs.AvaliacaoContracts;

public record AvaliacaoAtualizarDTO
{
    public int Nota { get; init; }
    public string Comentario { get; init; } = string.Empty;
}

namespace Markplace.Application.DTOs.AvaliacaoContracts;

public record AvaliacaoCriarDTO
{
    public int ProdutoId { get; init; }
    public int Nota { get; init; }
    public string Comentario { get; init; } = string.Empty;
}

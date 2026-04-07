namespace Markplace.Application.DTOs.ClienteContracts;

public record ClienteAtualizarPerfilDTO
{
    public string? Nome { get; init; }
    public string? Cpf { get; init; }
    public string? Telefone { get; init; }
}

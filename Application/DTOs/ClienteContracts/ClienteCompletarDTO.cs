namespace Markplace.Application.DTOs.ClienteContracts;

public record ClienteCompletarDTO
{
    public string Nome { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Telefone { get; init; } = string.Empty;
}

namespace Markplace.Application.DTOs.VendedorContracts;

public record VendedorCompletarDTO
{
    public string? Empresa { get; init; }
    public string Descricao { get; init; } = string.Empty;
    public string Cnpj { get; init; } = string.Empty;
    public string Telefone { get; init; } = string.Empty;
}

namespace Markplace.Application.DTOs.EnderecoContracts;

public record EnderecoDTO
{
    public string Rua { get; init; } = string.Empty;
    public string Numero { get; init; } = string.Empty;
    public string Bairro { get; init; } = string.Empty;
    public string Cidade { get; init; } = string.Empty;
    public string Estado { get; init; } = string.Empty;
    public string Cep { get; init; } = string.Empty;
}

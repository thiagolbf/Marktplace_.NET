namespace Markplace.Application.DTOs.EnderecoContracts;

public record EnderecoDetalheDTO
(
    int Id,
    string? Rua,
    string? Numero,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? Cep,
    DateTime CriadoEm
);

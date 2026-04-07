namespace Markplace.Application.DTOs.VendedorContracts;

public record VendedorPerfilDTO
(
    string? Empresa,
    string? Nome,
    string? Email,
    string? Telefone,
    string? Cnpj,
    bool Ativo,
    DateTime CriadoEm
);

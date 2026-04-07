namespace Markplace.Application.DTOs.ClienteContracts;

public record ClientePerfilDTO
(
    string? Nome,
    string? Cpf,
    string? Telefone,
    bool Ativo,
    DateTime CriadoEm,
    string? Email
);

using Markplace.Domain.Entities;

namespace Markplace.Application.DTOs.ClienteContracts;

public static class ClienteMappings
{
    public static Cliente ToEntity(this ClienteCompletarDTO clienteDto, string applicationUserId)
    {
        if (clienteDto is null)
            throw new ArgumentNullException(nameof(clienteDto));

        return new Cliente(
            clienteDto.Nome,
            clienteDto.Cpf,
            clienteDto.Telefone,
            applicationUserId
        );
    }

    public static ClienteDTO ToClienteDTO(this Cliente cliente)
    {
        if (cliente is null)
            throw new ArgumentNullException(nameof(cliente));

        return new ClienteDTO(
            cliente.Nome,
            cliente.CPF,
            cliente.Telefone
        );
    }

    public static ClientePerfilDTO ToClientePerfilDTO(this Cliente cliente)
    {
        if (cliente is null)
            throw new ArgumentNullException(nameof(cliente));

        return new ClientePerfilDTO(
            cliente.Nome,
            cliente.CPF,
            cliente.Telefone,
            cliente.Ativo,
            cliente.CriadoEm,
            cliente.ApplicationUser?.Email
        );
    }
}

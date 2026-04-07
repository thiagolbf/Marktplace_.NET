using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces;

public interface IClienteService
{
    Task CompletarCadastroAsync(Cliente cliente);
    Task<Cliente> ObterPerfilPorUsuarioAsync(string applicationUserId);
    Task AtualizarPerfilAsync(string applicationUserId, string? nome, string? cpf, string? telefone);
    Task<bool> AlternarStatusAtivoAsync(int clienteId);
}

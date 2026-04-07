using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces;

public interface IVendedorService
{
    Task CompletarCadastroAsync(Vendedor vendedor);
    Task<Vendedor> ObterPerfilPorUsuarioAsync(string applicationUserId);
    Task AtualizarPerfilAsync(string applicationUserId, string? nome, string? telefone);
    Task<bool> AlternarStatusAtivoAsync(int vendedorId);
}

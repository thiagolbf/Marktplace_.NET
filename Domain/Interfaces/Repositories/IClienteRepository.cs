using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<bool> ExistePorApplicationUserIdAsync(string applicationUserId);
    Task<bool> ExistePorCpfAsync(string cpf);
    Task<Cliente?> ObterPorApplicationUserIdAsync(string applicationUserId);
    Task<Cliente?> ObterPerfilPorApplicationUserIdAsync(string applicationUserId);
}

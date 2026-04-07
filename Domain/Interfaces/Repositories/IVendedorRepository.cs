using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories
{
public interface IVendedorRepository : IRepository<Vendedor>
{
    Task<bool> ExistePorApplicationUserIdAsync(string applicationUserId);
    Task<bool> ExistePorCnpjAsync(string cnpj);
    Task<Vendedor?> ObterPorApplicationUserIdAsync(string applicationUserId);
    Task<Vendedor?> ObterPerfilPorApplicationUserIdAsync(string applicationUserId);
}
}

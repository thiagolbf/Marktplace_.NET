using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<bool> ExistePorNomeAsync(string nome);
}

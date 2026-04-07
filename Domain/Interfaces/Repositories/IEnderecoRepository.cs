using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories;

public interface IEnderecoRepository : IRepository<Endereco>
{
    Task<IEnumerable<Endereco>> ObterPorClienteIdAsync(int clienteId);
    Task<Endereco?> ObterPorIdEClienteIdAsync(int id, int clienteId);
}

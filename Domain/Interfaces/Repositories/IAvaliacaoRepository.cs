using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories;

public interface IAvaliacaoRepository : IRepository<Avaliacao>
{
    Task<Avaliacao?> ObterPorClienteEProdutoAsync(int clienteId, int produtoId);
    Task<Avaliacao?> ObterPorIdEClienteAsync(int avaliacaoId, int clienteId);
    Task<IEnumerable<Avaliacao>> ObterPorProdutoAsync(int produtoId);
}

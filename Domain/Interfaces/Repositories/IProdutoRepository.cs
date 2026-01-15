using Markplace.Domain.Entities;

namespace Markplace.Domain.Interfaces.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> ObterProdutosPorVendedorAsync(int id);
}

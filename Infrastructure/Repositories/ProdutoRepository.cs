using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;

namespace Markplace.Infrastructure.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {}

    public async Task<IEnumerable<Produto>> ObterProdutosPorVendedorAsync(int id)
    {
        var produtos = await ObeterTodosAsync();
        var produtosCategoria = produtos.Where(c => c.VendedorId == id);
        return produtosCategoria;
    }
}

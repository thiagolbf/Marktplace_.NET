using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {}


    public override async Task<IEnumerable<Produto>> ObeterTodosAsync()
    {
        return await _context.Produtos
            .Include(p => p.Vendedor)
            .Include(p => p.ProdutoCategorias)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<Produto?> ObterPorIdComDetalhesAsync(int id)
    {
        return await _context.Produtos
            .Include(p => p.Vendedor)
            .Include(p => p.ProdutoCategorias)
                .ThenInclude(pc => pc.Categoria)
            //.Include(p => p.Avaliacoes)
            .FirstOrDefaultAsync(p => p.Id == id);

    }
    public async Task<IEnumerable<Produto>> ObterProdutosPorVendedorAsync(int id)
    {
        var produtos = await ObeterTodosAsync();
        var produtosCategoria = produtos.Where(c => c.VendedorId == id);
        return produtosCategoria;
    }
}

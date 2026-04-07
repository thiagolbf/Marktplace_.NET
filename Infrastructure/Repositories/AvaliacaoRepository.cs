using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repository;

public class AvaliacaoRepository : Repository<Avaliacao>, IAvaliacaoRepository
{
    public AvaliacaoRepository(AppDbContext context) : base(context) { }

    public async Task<Avaliacao?> ObterPorClienteEProdutoAsync(int clienteId, int produtoId)
    {
        return await _context.Avaliacoes
            .FirstOrDefaultAsync(a => a.ClienteId == clienteId && a.ProdutoId == produtoId);
    }

    public async Task<Avaliacao?> ObterPorIdEClienteAsync(int avaliacaoId, int clienteId)
    {
        return await _context.Avaliacoes
            .FirstOrDefaultAsync(a => a.Id == avaliacaoId && a.ClienteId == clienteId);
    }

    public async Task<IEnumerable<Avaliacao>> ObterPorProdutoAsync(int produtoId)
    {
        return await _context.Avaliacoes
            .Where(a => a.ProdutoId == produtoId)
            .AsNoTracking()
            .ToListAsync();
    }
}

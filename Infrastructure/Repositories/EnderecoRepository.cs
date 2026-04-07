using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repository;

public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
{
    public EnderecoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Endereco>> ObterPorClienteIdAsync(int clienteId)
    {
        return await _context.Enderecos
            .Where(e => e.ClienteId == clienteId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Endereco?> ObterPorIdEClienteIdAsync(int id, int clienteId)
    {
        return await _context.Enderecos
            .FirstOrDefaultAsync(e => e.Id == id && e.ClienteId == clienteId);
    }
}

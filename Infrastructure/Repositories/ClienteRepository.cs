using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repository;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistePorApplicationUserIdAsync(string applicationUserId)
    {
        return await _context.Clientes.AnyAsync(c => c.ApplicationUserId == applicationUserId);
    }

    public async Task<bool> ExistePorCpfAsync(string cpf)
    {
        return await _context.Clientes.AnyAsync(c => c.CPF == cpf);
    }

    public async Task<Cliente?> ObterPorApplicationUserIdAsync(string applicationUserId)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);
    }

    public async Task<Cliente?> ObterPerfilPorApplicationUserIdAsync(string applicationUserId)
    {
        return await _context.Clientes
            .Include(c => c.ApplicationUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId);
    }
}

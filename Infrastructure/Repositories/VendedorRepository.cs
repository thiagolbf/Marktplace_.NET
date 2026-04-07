using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repository
{
    public class VendedorRepository : Repository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistePorApplicationUserIdAsync(string applicationUserId)
        {
            return await _context.Vendedores.AnyAsync(v => v.ApplicationUserId == applicationUserId);
        }

        public async Task<bool> ExistePorCnpjAsync(string cnpj)
        {
            return await _context.Vendedores.AnyAsync(v => v.CNPJ == cnpj);
        }

    public async Task<Vendedor?> ObterPorApplicationUserIdAsync(string applicationUserId)
    {
        return await _context.Vendedores
            .FirstOrDefaultAsync(v => v.ApplicationUserId == applicationUserId);
    }

    public async Task<Vendedor?> ObterPerfilPorApplicationUserIdAsync(string applicationUserId)
    {
        return await _context.Vendedores
            .Include(v => v.ApplicationUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.ApplicationUserId == applicationUserId);
    }
}
}

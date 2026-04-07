using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistePorNomeAsync(string nome)
    {
        return await _context.Categorias.AnyAsync(c => c.Nome == nome);
    }
}

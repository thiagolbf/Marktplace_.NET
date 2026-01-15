using Markplace.Domain.Interfaces.UnitOfWork;
using Markplace.Infrastructure.Context;

namespace Markplace.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _context;
    
    public UnitOfWork (AppDbContext context)
    {
        _context = context;
    }


    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

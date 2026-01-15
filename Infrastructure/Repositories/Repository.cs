using Markplace.Domain.Interfaces.Repositories;
using Markplace.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Markplace.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> ObeterTodosAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
  
    }

    public async Task<T?> ObterPorIdAsync(int id)
    {
        //Ñ utilizado AsNoTracking pois pode ser necessário ter na memória para
        //atualização de dados
        return await _context.Set<T>().FindAsync(id);
        
    }
    public T Criar(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public T Atualizar(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }

    public T Remover(T entity)
    {
        _context.Set<T>().Remove(entity);
        return entity;
    }
}
//public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
//{
//    return await _context.Set<T>().FirstOrDefaultAsync(predicate);
//}


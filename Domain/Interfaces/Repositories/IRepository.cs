using Markplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Markplace.Domain.Interfaces.Repositories;
public interface IRepository<T>
{
    Task<IEnumerable<T>> ObeterTodosAsync();

    Task<T?> ObterPorIdAsync(int id);
    T Criar (T entity);
    T Atualizar (T entity);
    T Remover (T entity);
}



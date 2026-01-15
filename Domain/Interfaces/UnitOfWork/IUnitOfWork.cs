namespace Markplace.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();  
}

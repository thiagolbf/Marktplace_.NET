using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObterTodosAsync();
        Task<Categoria> ObterPorId(int id);
        Task Adicionar(Categoria categoria);
        Task Atualizar(int id, string nome);
        Task Remover(int id);
    }
}

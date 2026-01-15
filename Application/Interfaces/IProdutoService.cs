using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces;

public interface IProdutoService
{
    Task Adicionar(Produto produto);
    Task Atualizar(Produto produto);
    Task Remover(int id);
}

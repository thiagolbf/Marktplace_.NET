using Markplace.Application.DTOs.ProdutoContracts;
using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces;

public interface IProdutoService
{
    Task<Produto> Adicionar(ProdutoDTO produtoDto, string applicationUserId);
    Task Atualizar(int id, string nome, string descricao);
    Task AtualizarPreco(int id, decimal preco);
    Task Remover(int id);
    Task<IEnumerable<Produto>> ObterTodosAsync();
    Task<Produto> ObterPorId(int id);

}

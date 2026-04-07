using Markplace.Domain.Entities;

namespace Markplace.Application.Interfaces;

public interface IAvaliacaoService
{
    Task<Avaliacao> AdicionarAsync(string applicationUserId, int produtoId, int nota, string comentario);
    Task AtualizarAsync(string applicationUserId, int avaliacaoId, int nota, string comentario);
    Task RemoverAsync(string applicationUserId, int avaliacaoId);
    Task<IEnumerable<Avaliacao>> ObterPorProdutoAsync(int produtoId);
}

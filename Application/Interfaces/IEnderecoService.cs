using Markplace.Domain.Entities;
using Markplace.Domain.ValueObjects;

namespace Markplace.Application.Interfaces;

public interface IEnderecoService
{
    Task<IEnumerable<Endereco>> ObterMeusEnderecosAsync(string applicationUserId);
    Task<Endereco> ObterMeuEnderecoPorIdAsync(string applicationUserId, int enderecoId);
    Task<Endereco> AdicionarAsync(string applicationUserId, EnderecoValor enderecoValor);
    Task AtualizarAsync(string applicationUserId, int enderecoId, EnderecoValor enderecoValor);
    Task RemoverAsync(string applicationUserId, int enderecoId);
}

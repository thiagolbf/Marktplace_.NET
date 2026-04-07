using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class AvaliacaoService : IAvaliacaoService
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AvaliacaoService(
        IAvaliacaoRepository avaliacaoRepository,
        IClienteRepository clienteRepository,
        IProdutoRepository produtoRepository,
        IUnitOfWork unitOfWork)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _clienteRepository = clienteRepository;
        _produtoRepository = produtoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Avaliacao> AdicionarAsync(string applicationUserId, int produtoId, int nota, string comentario)
    {
        var cliente = await ObterClienteObrigatorio(applicationUserId);

        var produto = await _produtoRepository.ObterPorIdAsync(produtoId);
        if (produto is null) throw new NotFoundException("Produto nao encontrado.");

        var duplicada = await _avaliacaoRepository.ObterPorClienteEProdutoAsync(cliente.Id, produtoId);
        if (duplicada is not null) throw new DomainException("Cliente ja avaliou este produto.");

        var nova = new Avaliacao(nota, comentario, cliente.Id, produtoId);
        _avaliacaoRepository.Criar(nova);
        await _unitOfWork.CommitAsync();
        return nova;
    }

    public async Task AtualizarAsync(string applicationUserId, int avaliacaoId, int nota, string comentario)
    {
        if (avaliacaoId <= 0) throw new DomainException("Id da avaliacao invalido.");
        var cliente = await ObterClienteObrigatorio(applicationUserId);

        var avaliacao = await _avaliacaoRepository.ObterPorIdEClienteAsync(avaliacaoId, cliente.Id);
        if (avaliacao is null) throw new NotFoundException("Avaliacao nao encontrada.");

        avaliacao.Atualizar(nota, comentario);
        _avaliacaoRepository.Atualizar(avaliacao);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoverAsync(string applicationUserId, int avaliacaoId)
    {
        if (avaliacaoId <= 0) throw new DomainException("Id da avaliacao invalido.");
        var cliente = await ObterClienteObrigatorio(applicationUserId);

        var avaliacao = await _avaliacaoRepository.ObterPorIdEClienteAsync(avaliacaoId, cliente.Id);
        if (avaliacao is null) throw new NotFoundException("Avaliacao nao encontrada.");

        _avaliacaoRepository.Remover(avaliacao);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<Avaliacao>> ObterPorProdutoAsync(int produtoId)
    {
        if (produtoId <= 0) throw new DomainException("ProdutoId invalido.");
        return await _avaliacaoRepository.ObterPorProdutoAsync(produtoId);
    }

    private async Task<Cliente> ObterClienteObrigatorio(string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var cliente = await _clienteRepository.ObterPorApplicationUserIdAsync(applicationUserId);
        if (cliente is null) throw new NotFoundException("Perfil de cliente nao encontrado.");
        return cliente;
    }
}

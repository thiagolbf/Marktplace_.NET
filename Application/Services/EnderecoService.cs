using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;
using Markplace.Domain.ValueObjects;

namespace Markplace.Application.Services;

public class EnderecoService : IEnderecoService
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnderecoService(IEnderecoRepository enderecoRepository, IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _enderecoRepository = enderecoRepository;
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Endereco>> ObterMeusEnderecosAsync(string applicationUserId)
    {
        var cliente = await ObterClienteObrigatorio(applicationUserId);
        return await _enderecoRepository.ObterPorClienteIdAsync(cliente.Id);
    }

    public async Task<Endereco> ObterMeuEnderecoPorIdAsync(string applicationUserId, int enderecoId)
    {
        if (enderecoId <= 0) throw new DomainException("Id do endereco invalido.");
        var cliente = await ObterClienteObrigatorio(applicationUserId);
        var endereco = await _enderecoRepository.ObterPorIdEClienteIdAsync(enderecoId, cliente.Id);

        if (endereco is null)
            throw new NotFoundException("Endereco nao encontrado.");

        return endereco;
    }

    public async Task<Endereco> AdicionarAsync(string applicationUserId, EnderecoValor enderecoValor)
    {
        var cliente = await ObterClienteObrigatorio(applicationUserId);
        var endereco = new Endereco(cliente.Id, enderecoValor);

        _enderecoRepository.Criar(endereco);
        await _unitOfWork.CommitAsync();
        return endereco;
    }

    public async Task AtualizarAsync(string applicationUserId, int enderecoId, EnderecoValor enderecoValor)
    {
        if (enderecoId <= 0) throw new DomainException("Id do endereco invalido.");

        var cliente = await ObterClienteObrigatorio(applicationUserId);
        var endereco = await _enderecoRepository.ObterPorIdEClienteIdAsync(enderecoId, cliente.Id);
        if (endereco is null) throw new NotFoundException("Endereco nao encontrado.");

        endereco.Atualizar(enderecoValor);
        _enderecoRepository.Atualizar(endereco);
        await _unitOfWork.CommitAsync();
    }

    public async Task RemoverAsync(string applicationUserId, int enderecoId)
    {
        if (enderecoId <= 0) throw new DomainException("Id do endereco invalido.");

        var cliente = await ObterClienteObrigatorio(applicationUserId);
        var endereco = await _enderecoRepository.ObterPorIdEClienteIdAsync(enderecoId, cliente.Id);
        if (endereco is null) throw new NotFoundException("Endereco nao encontrado.");

        _enderecoRepository.Remover(endereco);
        await _unitOfWork.CommitAsync();
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

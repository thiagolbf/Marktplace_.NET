using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CompletarCadastroAsync(Cliente cliente)
    {
        if (cliente is null)
            throw new DomainException("Cliente invalido.");

        if (string.IsNullOrWhiteSpace(cliente.ApplicationUserId))
            throw new DomainException("Usuario invalido.");

        if (string.IsNullOrWhiteSpace(cliente.CPF))
            throw new DomainException("CPF invalido.");

        var clienteExistente = await _clienteRepository.ExistePorApplicationUserIdAsync(cliente.ApplicationUserId);
        if (clienteExistente)
            throw new DomainException("Cadastro de cliente ja foi concluido para este usuario.");

        var cpfExistente = await _clienteRepository.ExistePorCpfAsync(cliente.CPF);
        if (cpfExistente)
            throw new DomainException("Ja existe cliente cadastrado com este CPF.");

        _clienteRepository.Criar(cliente);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Cliente> ObterPerfilPorUsuarioAsync(string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var cliente = await _clienteRepository.ObterPerfilPorApplicationUserIdAsync(applicationUserId);
        if (cliente is null)
            throw new NotFoundException("Perfil de cliente nao encontrado.");

        return cliente;
    }

    public async Task AtualizarPerfilAsync(string applicationUserId, string? nome, string? cpf, string? telefone)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var cliente = await _clienteRepository.ObterPorApplicationUserIdAsync(applicationUserId);
        if (cliente is null)
            throw new NotFoundException("Perfil de cliente nao encontrado.");

        if (!string.IsNullOrWhiteSpace(cpf))
        {
            var cpfExistente = await _clienteRepository.ExistePorCpfAsync(cpf);
            if (cpfExistente && !string.Equals(cliente.CPF, cpf, StringComparison.Ordinal))
                throw new DomainException("Ja existe cliente cadastrado com este CPF.");
        }

        cliente.AtualizarPerfil(nome, cpf, telefone);
        _clienteRepository.Atualizar(cliente);
        await _unitOfWork.CommitAsync();
    }

    public async Task<bool> AlternarStatusAtivoAsync(int clienteId)
    {
        if (clienteId <= 0)
            throw new DomainException("Id do cliente invalido.");

        var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
        if (cliente is null)
            throw new NotFoundException("Cliente nao encontrado.");

        var ativo = cliente.AlternarStatusAtivo();
        _clienteRepository.Atualizar(cliente);
        await _unitOfWork.CommitAsync();

        return ativo;
    }
}

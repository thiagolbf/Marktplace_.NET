using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class VendedorService : IVendedorService
{
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VendedorService(IVendedorRepository vendedorRepository, IUnitOfWork unitOfWork)
    {
        _vendedorRepository = vendedorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CompletarCadastroAsync(Vendedor vendedor)
    {
        if (vendedor is null)
            throw new DomainException("Vendedor invalido.");

        if (string.IsNullOrWhiteSpace(vendedor.ApplicationUserId))
            throw new DomainException("Usuario invalido.");

        if (string.IsNullOrWhiteSpace(vendedor.CNPJ))
            throw new DomainException("CNPJ invalido.");

        var vendedorExistente = await _vendedorRepository.ExistePorApplicationUserIdAsync(vendedor.ApplicationUserId);
        if (vendedorExistente)
            throw new DomainException("Cadastro de vendedor ja foi concluido para este usuario.");

        var cnpjExistente = await _vendedorRepository.ExistePorCnpjAsync(vendedor.CNPJ);
        if (cnpjExistente)
            throw new DomainException("Ja existe vendedor cadastrado com este CNPJ.");

        _vendedorRepository.Criar(vendedor);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Vendedor> ObterPerfilPorUsuarioAsync(string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var vendedor = await _vendedorRepository.ObterPerfilPorApplicationUserIdAsync(applicationUserId);
        if (vendedor is null)
            throw new NotFoundException("Perfil de vendedor nao encontrado.");

        return vendedor;
    }

    public async Task AtualizarPerfilAsync(string applicationUserId, string? nome, string? telefone)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var vendedor = await _vendedorRepository.ObterPorApplicationUserIdAsync(applicationUserId);

        if (vendedor is null)
            throw new NotFoundException("Perfil de vendedor nao encontrado.");

        vendedor.AtualizarPerfil(nome, telefone);

        _vendedorRepository.Atualizar(vendedor);
        await _unitOfWork.CommitAsync();
    }

    public async Task<bool> AlternarStatusAtivoAsync(int vendedorId)
    {
        if (vendedorId <= 0)
            throw new DomainException("Id do vendedor invalido.");

        var vendedor = await _vendedorRepository.ObterPorIdAsync(vendedorId);

        if (vendedor is null)
            throw new NotFoundException("Vendedor nao encontrado.");

        var statusAtual = vendedor.AlternarStatusAtivo();

        _vendedorRepository.Atualizar(vendedor);
        await _unitOfWork.CommitAsync();

        return statusAtual;
    }
}

using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepositoy;
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepositoy = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Adicionar(Categoria categoria)
    {
        if (categoria is null)
            throw new DomainException("Categoria invalida.");

        var nome = categoria.Nome?.ToLowerInvariant() ?? string.Empty;

        var existe = await _categoriaRepositoy.ExistePorNomeAsync(nome);
        if (existe)
            throw new DomainException("Ja existe uma categoria com esse nome.");

        _categoriaRepositoy.Criar(categoria);
        await _unitOfWork.CommitAsync();
    }

    public async Task Atualizar(int id, string descricao)
    {
        if (id <= 0)
            throw new DomainException("Id invalido.");

        var categoria = await _categoriaRepositoy.ObterPorIdAsync(id);

        if (categoria == null)
            throw new NotFoundException($"Categoria com id {id} nao encontrada.");

        categoria.Atualizar(descricao);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Categoria> ObterPorId(int id)
    {
        if (id <= 0)
            throw new DomainException("Id invalido.");

        var categoria = await _categoriaRepositoy.ObterPorIdAsync(id);

        if (categoria is null)
            throw new NotFoundException($"Categoria com id {id} nao encontrada.");

        return categoria;
    }

    public async Task<IEnumerable<Categoria>> ObterTodosAsync()
    {
        return await _categoriaRepositoy.ObeterTodosAsync();
    }

    public async Task Remover(int id)
    {
        if (id <= 0)
            throw new DomainException("Id invalido.");

        var categoria = await _categoriaRepositoy.ObterPorIdAsync(id);

        if (categoria == null)
            throw new NotFoundException("Categoria nao encontrada.");

        _categoriaRepositoy.Remover(categoria);

        await _unitOfWork.CommitAsync();
    }
}

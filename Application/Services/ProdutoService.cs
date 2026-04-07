using Markplace.Application.DTOs.ProdutoContracts;
using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProdutoService(
        IProdutoRepository produtoRepository,
        IVendedorRepository vendedorRepository,
        IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        _vendedorRepository = vendedorRepository ?? throw new ArgumentException(nameof(vendedorRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Produto> Adicionar(ProdutoDTO produtoDto, string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new DomainException("Usuario invalido.");

        var vendedor = await _vendedorRepository.ObterPorApplicationUserIdAsync(applicationUserId);
        if (vendedor == null)
            throw new NotFoundException("Perfil de vendedor nao encontrado.");

        var produto = new Produto(
            produtoDto.Nome,
            produtoDto.Descricao,
            produtoDto.Preco,
            produtoDto.Quantidade,
            vendedor.Id
        );

        produto.AdicionarCategorias(produtoDto.CategoriasIds);

        _produtoRepository.Criar(produto);
        await _unitOfWork.CommitAsync();
        return produto;
    }

    public async Task Atualizar(int id, string nome, string descricao)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto == null)
            throw new NotFoundException($"Produto com ID {id} não encontrado.");

        produto.AtualizarDados(nome, descricao);

        _produtoRepository.Atualizar(produto);

        await _unitOfWork.CommitAsync();
    }

    public async Task AtualizarPreco(int produtoId, decimal novoPreco)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

        if (produto == null)
            throw new NotFoundException($"Produto com ID {produtoId} não encontrado.");

        produto.AtualizarPreco(novoPreco);

        _produtoRepository.Atualizar(produto);
        await _unitOfWork.CommitAsync();
    }

    public async Task Remover(int produtoId)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

        if (produto == null)
            throw new NotFoundException($"Produto com ID {produtoId} não encontrado.");

        produto.Desativar();

        _produtoRepository.Atualizar(produto);
        await _unitOfWork.CommitAsync();
    }

    public async Task<Produto> ObterPorId(int id)
    {
        if (id <= 0)
            throw new DomainException("Id inválido.");

        var produto = await _produtoRepository.ObterPorIdComDetalhesAsync(id);

        if (produto is null)
            throw new NotFoundException($"Produto com ID {id} não encontrado.");

        return produto;
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _produtoRepository.ObeterTodosAsync();
    }
}

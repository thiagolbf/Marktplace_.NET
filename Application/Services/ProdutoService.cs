using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;

namespace Markplace.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProdutoService(IProdutoRepository produtoRepository, IVendedorRepository vendedorRepository,
                            IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        _vendedorRepository = vendedorRepository ?? throw new ArgumentException(nameof(vendedorRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Adicionar(Produto produto)
    {

        var vendedor = await _vendedorRepository.ObterPorIdAsync(produto.VendedorId);

        if (vendedor == null) 
            throw new Exception($"Vendedor com ID {produto.VendedorId} não existe.");

        _produtoRepository.Criar(produto);
        await _unitOfWork.CommitAsync();
      
    }

    public Task Atualizar(Produto produto)
    {
        throw new NotImplementedException();
    }

    public Task Remover(int id)
    {
        throw new NotImplementedException();
    }
}


/*
 * 
 
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
    {
        _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Adicionar(Produto produto)
    {
        _produtoRepository.Criar(produto);
        await _unitOfWork.CommitAsync();
    }

    public async Task Atualizar(Produto produto)
    {
        _produtoRepository.Atualizar(produto);
        await _unitOfWork.CommitAsync();
    }

    public async Task Remover(int id)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(id);

        if (produto == null)
            throw new Exception($"Produto com ID {id} não foi encontrado.");

        produto.Desativar();

        _produtoRepository.Atualizar(produto);
        await _unitOfWork.CommitAsync();
        // AQUI UTILIZA ATUALIZAR PORQUE É UM SOFT DELETE e não um HARD DELETE
        ESSE É O PONTO DE VIOLAÇÃO DE UM PRINCIO SOLID
    }
}
 */

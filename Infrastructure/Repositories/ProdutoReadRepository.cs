using Markplace.Application.DTOs.AvaliacaoContracts;
using Markplace.Application.DTOs.ProdutoContracts;
using Markplace.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Markplace.Infrastructure.Repositories;

//public class ProdutoReadRepository
//{
//    private readonly AppDbContext _context;

//    public ProdutoReadRepository(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<ProdutoAdminDTO?> ObterDetalheAsync(int id)
//    {
//        return await _context.Produtos
//            .Where(p => p.Id == id)
//            .Select(p => new ProdutoAdminDTO
//            {
//                Id = p.Id,
//                Nome = p.Nome,
//                Empresa = p.Vendedor!.Empresa,

//                Categorias = p.ProdutoCategorias
//                    .Select(pc => pc.Categoria.Nome)
//                    .ToList(),

//                Avaliacoes = _context.Avaliacoes
//                    .Where(a => a.ProdutoId == p.Id)
//                    .Select(a => new AvaliacaoDTO(
//                        a.Nota,
//                        a.Comentario
//                    ))
//                    .ToList()
//            })
//            .FirstOrDefaultAsync();
//    }
//}

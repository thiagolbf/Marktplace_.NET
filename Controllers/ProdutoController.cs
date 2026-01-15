using Markplace.Application.DTOs;
using Markplace.Application.DTOs.Mappings;
using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
    }
    
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProdutoDTO produtoEntrada)
    {
        try
        {

            

            //var produto = new Produto(
            //produtoEntrada.Nome,
            //produtoEntrada.Descricao,
            //produtoEntrada.Preco,
            //produtoEntrada.Quantidade,
            //produtoEntrada.VendedorId
            //);

            var produto = ProdutoDTOExtensions.ToProduto(produtoEntrada);

            //return CreatedAtAction(nameof(Adicionar), produto);

            await _produtoService.Adicionar(produto);
           
            return Created("", new
            {
                produto.Id,
                produto.Nome,
                produto.Preco,
                produto.Quantidade,
                produto.VendedorId
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                mensagem = ex.InnerException?.Message ?? ex.Message
            });
        }

    }
}
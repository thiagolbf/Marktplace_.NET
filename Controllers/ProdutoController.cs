using FluentValidation;
using Markplace.Application.DTOs.ProdutoContracts;
using Markplace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IValidator<ProdutoDTO> _validator;
    private readonly IValidator<ProdutoAtualizarDTO> _validacaoAtualizar;
    private readonly IValidator<ProdutoAtualizarPrecoDTO> _validacaoAtualizacaoPreco;
    private readonly IProdutoService _produtoService;

    public ProdutoController(
        IValidator<ProdutoDTO> validator,
        IValidator<ProdutoAtualizarPrecoDTO> validacaoAtualizacaoPreco,
        IValidator<ProdutoAtualizarDTO> validacaoAtualizar,
        IProdutoService produtoService)
    {
        _produtoService = produtoService;
        _validator = validator;
        _validacaoAtualizar = validacaoAtualizar;
        _validacaoAtualizacaoPreco = validacaoAtualizacaoPreco;
    }

    [Authorize(Roles = "Vendedor, Cliente, Admin")]
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var produtos = await _produtoService.ObterTodosAsync();

        if (produtos == null || !produtos.Any())
            return NoContent();

        return Ok(produtos.ToProdutoResponseDTOList());
    }

    [Authorize(Roles = "Vendedor")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscaPorId(int id)
    {
        var produto = await _produtoService.ObterPorId(id);
        return Ok(produto.ToProdutoResponseDTO());
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] ProdutoDTO produtodto)
    {
        var validationResult = await _validator.ValidateAsync(produtodto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var produto = await _produtoService.Adicionar(produtodto, applicationUserId);

        var produtoResponse = produto.ToProdutoDTO();

        return StatusCode(StatusCodes.Status201Created, produtoResponse);
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoAtualizarDTO atualizardto)
    {
        var validationResult = await _validacaoAtualizar.ValidateAsync(atualizardto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        await _produtoService.Atualizar(id, atualizardto.Nome, atualizardto.Descricao);

        return NoContent();
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPatch("{id:int}/preco")]
    public async Task<IActionResult> AtualizarPreco(int id, [FromBody] ProdutoAtualizarPrecoDTO atualizarPrecoDto)
    {
        var validationResult = await _validacaoAtualizacaoPreco.ValidateAsync(atualizarPrecoDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        await _produtoService.AtualizarPreco(id, atualizarPrecoDto.Preco);

        return NoContent();
    }

    [Authorize(Roles = "Vendedor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Desativar(int id)
    {
        await _produtoService.Remover(id);
        return NoContent();
    }
}

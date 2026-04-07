using FluentValidation;
using Markplace.Application.DTOs.AvaliacaoContracts;
using Markplace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvaliacaoController : ControllerBase
{
    private readonly IValidator<AvaliacaoCriarDTO> _validatorCriar;
    private readonly IValidator<AvaliacaoAtualizarDTO> _validatorAtualizar;
    private readonly IAvaliacaoService _avaliacaoService;

    public AvaliacaoController(
        IValidator<AvaliacaoCriarDTO> validatorCriar,
        IValidator<AvaliacaoAtualizarDTO> validatorAtualizar,
        IAvaliacaoService avaliacaoService)
    {
        _validatorCriar = validatorCriar;
        _validatorAtualizar = validatorAtualizar;
        _avaliacaoService = avaliacaoService;
    }

    [HttpGet("produto/{produtoId:int}")]
    public async Task<IActionResult> ObterPorProduto(int produtoId)
    {
        var avaliacoes = await _avaliacaoService.ObterPorProdutoAsync(produtoId);
        if (!avaliacoes.Any()) return NoContent();
        return Ok(avaliacoes.ToDetalheDTOList());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AvaliacaoCriarDTO dto)
    {
        var validation = await _validatorCriar.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors.Select(e => new { Campo = e.PropertyName, Erro = e.ErrorMessage }));
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var avaliacao = await _avaliacaoService.AdicionarAsync(userId, dto.ProdutoId, dto.Nota, dto.Comentario);
        return StatusCode(StatusCodes.Status201Created, avaliacao.ToDetalheDTO());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AvaliacaoAtualizarDTO dto)
    {
        var validation = await _validatorAtualizar.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors.Select(e => new { Campo = e.PropertyName, Erro = e.ErrorMessage }));
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _avaliacaoService.AtualizarAsync(userId, id, dto.Nota, dto.Comentario);
        return NoContent();
    }

    [Authorize(Roles = "Cliente")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _avaliacaoService.RemoverAsync(userId, id);
        return NoContent();
    }
}

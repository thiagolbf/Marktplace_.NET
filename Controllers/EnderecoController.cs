using FluentValidation;
using Markplace.Application.DTOs.EnderecoContracts;
using Markplace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerBase
{
    private readonly IValidator<EnderecoDTO> _validator;
    private readonly IEnderecoService _enderecoService;

    public EnderecoController(IValidator<EnderecoDTO> validator, IEnderecoService enderecoService)
    {
        _validator = validator;
        _enderecoService = enderecoService;
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> ObterMeusEnderecos()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var enderecos = await _enderecoService.ObterMeusEnderecosAsync(userId);
        if (!enderecos.Any()) return NoContent();
        return Ok(enderecos.ToDetalheDTOList());
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterMeuEnderecoPorId(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var endereco = await _enderecoService.ObterMeuEnderecoPorIdAsync(userId, id);
        return Ok(endereco.ToDetalheDTO());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] EnderecoDTO dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors.Select(e => new { Campo = e.PropertyName, Erro = e.ErrorMessage }));
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var endereco = await _enderecoService.AdicionarAsync(userId, dto.ToValueObject());
        return StatusCode(StatusCodes.Status201Created, endereco.ToDetalheDTO());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] EnderecoDTO dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
        {
            return BadRequest(validation.Errors.Select(e => new { Campo = e.PropertyName, Erro = e.ErrorMessage }));
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _enderecoService.AtualizarAsync(userId, id, dto.ToValueObject());
        return NoContent();
    }

    [Authorize(Roles = "Cliente")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _enderecoService.RemoverAsync(userId, id);
        return NoContent();
    }
}

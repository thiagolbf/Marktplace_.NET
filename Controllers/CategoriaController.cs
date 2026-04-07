using FluentValidation;
using Markplace.Application.DTOs.CategoriaContracts;
using Markplace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IValidator<CategoriaDTO> _validator;
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(IValidator<CategoriaDTO> validator, ICategoriaService categoriaService)
    {
        _validator = validator;
        _categoriaService = categoriaService;
    }

    [Authorize(Roles = "Vendedor")]
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var categorias = await _categoriaService.ObterTodosAsync();

        if (categorias == null || !categorias.Any())
            return NoContent();

        return Ok(categorias.ToCategoriaDTOList());
    }

    [Authorize(Roles = "Vendedor")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscaPorId(int id)
    {
        var categoria = await _categoriaService.ObterPorId(id);
        return Ok(categoria.ToCategoriaDetalheDTO());
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CategoriaDTO categoriadto)
    {
        var validationResult = await _validator.ValidateAsync(categoriadto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var categoria = categoriadto.ToEntity();
        await _categoriaService.Adicionar(categoria);

        var categoriaResponse = categoria.ToCategoriaDTO();

        return StatusCode(StatusCodes.Status201Created, categoriaResponse);
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CategoriaDTO categoriadto)
    {
        var validationResult = await _validator.ValidateAsync(categoriadto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        await _categoriaService.Atualizar(id, categoriadto.Nome);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        await _categoriaService.Remover(id);
        return NoContent();
    }
}

using FluentValidation;
using Markplace.Application.DTOs.CategoriaContracts;
using Markplace.Application.DTOs.RoleContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Markplace.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IValidator<RoleDTO> _validator;
    private readonly RoleManager<IdentityRole> _roleManager;


    public RoleController(IValidator<RoleDTO> validator, RoleManager<IdentityRole> roleManager)
    {
        _validator = validator;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
    {

        var validationResult = await _validator.ValidateAsync(roleDTO);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        if (string.IsNullOrWhiteSpace(roleDTO.Nome))
            return BadRequest("Nome da role é obrigatório.");

        var exists = await _roleManager.RoleExistsAsync(roleDTO.Nome);

        if (exists)
            return BadRequest("Role já existe.");

        var result = await _roleManager.CreateAsync(
            new IdentityRole(roleDTO.Nome));

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"Role {roleDTO.Nome} criada com sucesso.");
    }

    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = _roleManager.Roles
            .Select(r => new { r.Id, r.Name })
            .ToList();

        return Ok(roles);
    }

    [HttpDelete("{roleName}")]
    public async Task<IActionResult> DeleteRole(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role == null)
            return NotFound("Role não encontrada.");

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok($"Role {roleName} removida com sucesso.");
    }

}


using FluentValidation;
using Markplace.Application.DTOs.VendedorContracts;
using Markplace.Application.Interfaces;
using Markplace.Domain.Entities;
using Markplace.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Markplace.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendedorController : ControllerBase
{
    private const string ProfileCompletedClaimType = "ProfileCompleted";
    private const string PermissionClaimType = "permission";
    private const string SellProductsPermission = "products.sell";

    private readonly IValidator<VendedorCompletarDTO> _validator;
    private readonly IValidator<VendedorAtualizarPerfilDTO> _validatorAtualizarPerfil;
    private readonly IVendedorService _vendedorService;
    private readonly UserManager<ApplicationUser> _userManager;

    public VendedorController(
        IValidator<VendedorCompletarDTO> validator,
        IValidator<VendedorAtualizarPerfilDTO> validatorAtualizarPerfil,
        IVendedorService vendedorService,
        UserManager<ApplicationUser> userManager)
    {
        _validator = validator;
        _validatorAtualizarPerfil = validatorAtualizarPerfil;
        _vendedorService = vendedorService;
        _userManager = userManager;
    }

    [Authorize(Roles = "Vendedor")]
    [HttpGet("meu-perfil")]
    public async Task<IActionResult> ObterMeuPerfil()
    {
        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var vendedor = await _vendedorService.ObterPerfilPorUsuarioAsync(applicationUserId);
        return Ok(vendedor.ToVendedorPerfilDTO());
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPatch("meu-perfil")]
    public async Task<IActionResult> AtualizarMeuPerfil([FromBody] VendedorAtualizarPerfilDTO atualizarDto)
    {
        var validationResult = await _validatorAtualizarPerfil.ValidateAsync(atualizarDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _vendedorService.AtualizarPerfilAsync(applicationUserId, atualizarDto.Nome, atualizarDto.Telefone);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> AlternarStatus(int id)
    {
        var ativo = await _vendedorService.AlternarStatusAtivoAsync(id);

        return Ok(new
        {
            vendedorId = id,
            ativo
        });
    }

    [Authorize(Roles = "Vendedor")]
    [HttpPost("completar")]
    public async Task<IActionResult> CompletarCadastro([FromBody] VendedorCompletarDTO vendedorDto)
    {
        var validationResult = await _validator.ValidateAsync(vendedorDto);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var vendedor = vendedorDto.ToEntity(applicationUserId);

        await _vendedorService.CompletarCadastroAsync(vendedor);

        var user = await _userManager.FindByIdAsync(applicationUserId)
            ?? throw new NotFoundException("Usuario nao encontrado.");

        var existingClaims = await _userManager.GetClaimsAsync(user);

        var profileCompleted = existingClaims.FirstOrDefault(c => c.Type == ProfileCompletedClaimType);
        if (profileCompleted is null || !string.Equals(profileCompleted.Value, "true", StringComparison.OrdinalIgnoreCase))
        {
            if (profileCompleted is not null)
                await _userManager.RemoveClaimAsync(user, profileCompleted);

            var addProfileClaim = await _userManager.AddClaimAsync(user, new Claim(ProfileCompletedClaimType, "true"));
            if (!addProfileClaim.Succeeded)
                throw new DomainException("Nao foi possivel adicionar claim de perfil completo.");
        }

        var hasSellPermission = existingClaims.Any(c =>
            c.Type == PermissionClaimType && c.Value == SellProductsPermission);

        if (!hasSellPermission)
        {
            var addPermissionClaim = await _userManager.AddClaimAsync(user, new Claim(PermissionClaimType, SellProductsPermission));
            if (!addPermissionClaim.Succeeded)
                throw new DomainException("Nao foi possivel adicionar claim de permissao.");
        }

        await _userManager.UpdateSecurityStampAsync(user);

        return StatusCode(StatusCodes.Status201Created, new
        {
            vendedor = vendedor.ToVendedorDTO(),
            mensagem = "Cadastro concluido. Faca login novamente para receber as novas claims."
        });
    }
}

using FluentValidation;
using Markplace.Application.DTOs.ClienteContracts;
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
public class ClienteController : ControllerBase
{
    private const string ProfileCompletedClaimType = "ProfileCompleted";

    private readonly IValidator<ClienteCompletarDTO> _validatorCompletar;
    private readonly IValidator<ClienteAtualizarPerfilDTO> _validatorAtualizar;
    private readonly IClienteService _clienteService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ClienteController(
        IValidator<ClienteCompletarDTO> validatorCompletar,
        IValidator<ClienteAtualizarPerfilDTO> validatorAtualizar,
        IClienteService clienteService,
        UserManager<ApplicationUser> userManager)
    {
        _validatorCompletar = validatorCompletar;
        _validatorAtualizar = validatorAtualizar;
        _clienteService = clienteService;
        _userManager = userManager;
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet("meu-perfil")]
    public async Task<IActionResult> ObterMeuPerfil()
    {
        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cliente = await _clienteService.ObterPerfilPorUsuarioAsync(applicationUserId);
        return Ok(cliente.ToClientePerfilDTO());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPatch("meu-perfil")]
    public async Task<IActionResult> AtualizarMeuPerfil([FromBody] ClienteAtualizarPerfilDTO dto)
    {
        var validationResult = await _validatorAtualizar.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        await _clienteService.AtualizarPerfilAsync(applicationUserId, dto.Nome, dto.Cpf, dto.Telefone);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> AlternarStatus(int id)
    {
        var ativo = await _clienteService.AlternarStatusAtivoAsync(id);
        return Ok(new { clienteId = id, ativo });
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost("completar")]
    public async Task<IActionResult> CompletarCadastro([FromBody] ClienteCompletarDTO dto)
    {
        var validationResult = await _validatorCompletar.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                Campo = e.PropertyName,
                Erro = e.ErrorMessage
            }));
        }

        var applicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cliente = dto.ToEntity(applicationUserId);
        await _clienteService.CompletarCadastroAsync(cliente);

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

        await _userManager.UpdateSecurityStampAsync(user);

        return StatusCode(StatusCodes.Status201Created, new
        {
            cliente = cliente.ToClienteDTO(),
            mensagem = "Cadastro concluido. Faca login novamente para receber as novas claims."
        });
    }
}

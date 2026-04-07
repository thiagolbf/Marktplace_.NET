using FluentValidation;
using Markplace.Application.DTOs.AuthContracts;
using Markplace.Application.DTOs.RoleContracts;
using Markplace.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Markplace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<RegisterDTO> _validator;
        private readonly IValidator<LoginDTO> _validatorlogin;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            IValidator<RegisterDTO> validator, 
            IValidator<LoginDTO> validatorlogin,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _validator = validator;
            _validatorlogin = validatorlogin;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            var validationResult = await _validator.ValidateAsync(registerDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Erro = e.ErrorMessage
                }));
            }

            if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
                return BadRequest("Role inválida.");

            var user = new ApplicationUser
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Senha);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, registerDTO.Role);

            return Ok("Usuário criado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null)
                return Unauthorized("Email ou senha inválidos");

            var senhaValida = await _userManager.CheckPasswordAsync(user, loginDTO.Senha);

            if (!senhaValida)
                return Unauthorized("Email ou senha inválidos");

            var roles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("security_stamp", user.SecurityStamp),
            };

            // Adiciona roles como claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Adiciona claims que estão salvas no banco
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["jwt:key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [Authorize]
        [HttpPost("forcar-logout")]
        public async Task<IActionResult> ForcarLogout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("Usuário não identificado.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            await _userManager.UpdateSecurityStampAsync(user);

            return Ok("SecurityStamp atualizado. Token atual invalidado.");
        }



    }
}

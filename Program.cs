using FluentValidation;
using Markplace.Application.DTOs.AuthContracts;
using Markplace.Application.DTOs.AvaliacaoContracts.Validator;
using Markplace.Application.DTOs.ClienteContracts.Validator;
using Markplace.Application.DTOs.CategoriaContracts;
using Markplace.Application.DTOs.EnderecoContracts.Validator;
using Markplace.Application.DTOs.ProdutoContracts.Validator;
using Markplace.Application.DTOs.RoleContracts.Validator;
using Markplace.Application.DTOs.VendedorContracts.Validator;
using Markplace.Application.Interfaces;
using Markplace.Application.Services;
using Markplace.Domain.Entities;
using Markplace.Domain.Interfaces.Repositories;
using Markplace.Domain.Interfaces.UnitOfWork;
using Markplace.Infrastructure.Context;
using Markplace.Infrastructure.Middlewares;
using Markplace.Infrastructure.Repositories;
using Markplace.Infrastructure.Repository;
using Markplace.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

// Repositories
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


// Services
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IVendedorService, VendedorService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddValidatorsFromAssemblyContaining<RoleDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDTO>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDTO>();

builder.Services.AddValidatorsFromAssemblyContaining<ProdutoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoAtualizarDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoAtualizarPrecoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaDTO>();
builder.Services.AddValidatorsFromAssemblyContaining<VendedorCompletarDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<VendedorAtualizarPerfilDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteCompletarDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClienteAtualizarPerfilDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EnderecoDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AvaliacaoCriarDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AvaliacaoAtualizarDTOValidator>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).
AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("jwt");

    var keyString = jwtSettings["Key"] ?? throw new ArgumentNullException("Jwt:Key não configurado");

    var key = Encoding.UTF8.GetBytes(keyString);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };


options.Events = new JwtBearerEvents
{
    OnTokenValidated = async context =>
    {
        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
        var userId = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            context.HttpContext.Items["AuthError"] = "Token inválido.";
            context.Fail("Token inválido.");
            return; // <-- importante: retornar para não continuar
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            context.HttpContext.Items["AuthError"] = "Usuário não encontrado.";
            context.Fail("Usuário não encontrado.");
            return;
        }

        var tokenStamp = context.Principal.FindFirst("security_stamp")?.Value;
        if (tokenStamp != user.SecurityStamp)
        {
            context.HttpContext.Items["AuthError"] = "Token expirado por alteração de segurança.";
            context.Fail("Token expirado.");
        }
    },

    OnChallenge = async context =>
    {
        context.HandleResponse();
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        // Pega a mensagem customizada, senão usa fallback
        var errorMessage = context.HttpContext.Items["AuthError"] as string
            ?? context.ErrorDescription
            ?? "Acesso não autorizado.";

        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            message = errorMessage
        });

        await context.Response.WriteAsync(result);
    }
};
});

 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.UseSwagger();     // gera o swagger.json
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


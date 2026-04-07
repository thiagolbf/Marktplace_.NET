using FluentValidation;
using Markplace.Application.DTOs.CategoriaContracts;
using System.Data;

namespace Markplace.Application.DTOs.RoleContracts.Validator;

public class RoleDTOValidator : AbstractValidator<RoleDTO>
{
    public RoleDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
                .WithMessage("O nome da atribuição não pode estar vazio.")
            .MaximumLength(50)
                .WithMessage("O nome da atribuição deve ter no máximo 50 caracteres.");
    }
}

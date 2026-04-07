using FluentValidation;

namespace Markplace.Application.DTOs.AuthContracts.Validator;

public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDTOValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email é obrigatório.")
        .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8)
            .WithMessage("A senha deve ter no mínimo 8 caracteres.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role é obrigatória.")
            .Must(role => role == "Cliente"
                       || role == "Vendedor"
                       || role == "Admin")
            .WithMessage("Role inválida. Use: Cliente ou Vendedor.");
    }
}


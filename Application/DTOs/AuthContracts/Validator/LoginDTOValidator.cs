using FluentValidation;

namespace Markplace.Application.DTOs.AuthContracts.Validator;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.Email)
       .NotEmpty().WithMessage("Email é obrigatório.")
       .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8)
            .WithMessage("A senha deve ter no mínimo 8 caracteres.");
    }
}

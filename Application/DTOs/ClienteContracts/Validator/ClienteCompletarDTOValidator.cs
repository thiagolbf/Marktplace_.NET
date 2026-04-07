using FluentValidation;

namespace Markplace.Application.DTOs.ClienteContracts.Validator;

public class ClienteCompletarDTOValidator : AbstractValidator<ClienteCompletarDTO>
{
    public ClienteCompletarDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome e obrigatorio.")
            .MaximumLength(150).WithMessage("Nome deve ter no maximo 150 caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("CPF e obrigatorio.")
            .Length(11).WithMessage("CPF deve conter 11 caracteres.")
            .Matches("^[0-9]+$").WithMessage("CPF deve conter apenas numeros.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone e obrigatorio.")
            .Length(11).WithMessage("Telefone deve conter 11 caracteres.")
            .Matches("^[0-9]+$").WithMessage("Telefone deve conter apenas numeros.");
    }
}

using FluentValidation;

namespace Markplace.Application.DTOs.VendedorContracts.Validator;

public class VendedorAtualizarPerfilDTOValidator : AbstractValidator<VendedorAtualizarPerfilDTO>
{
    public VendedorAtualizarPerfilDTOValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Nome) || !string.IsNullOrWhiteSpace(x.Telefone))
            .WithMessage("Informe nome e/ou telefone para atualizar.");

        RuleFor(x => x.Nome)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.Nome))
            .WithMessage("Nome deve ter no maximo 150 caracteres.");

        RuleFor(x => x.Telefone)
            .Length(11)
            .When(x => !string.IsNullOrWhiteSpace(x.Telefone))
            .WithMessage("Telefone deve conter 11 caracteres.")
            .Matches("^[0-9]+$")
            .When(x => !string.IsNullOrWhiteSpace(x.Telefone))
            .WithMessage("Telefone deve conter apenas numeros.");
    }
}

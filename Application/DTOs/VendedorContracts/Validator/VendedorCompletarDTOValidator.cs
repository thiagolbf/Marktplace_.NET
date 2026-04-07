using FluentValidation;

namespace Markplace.Application.DTOs.VendedorContracts.Validator;

public class VendedorCompletarDTOValidator : AbstractValidator<VendedorCompletarDTO>
{
    public VendedorCompletarDTOValidator()
    {
        RuleFor(x => x.Empresa)
            .MaximumLength(150)
            .WithMessage("Empresa deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória.")
            .MaximumLength(150).WithMessage("Descrição deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("CNPJ é obrigatório.")
            .Length(14).WithMessage("CNPJ deve conter 14 caracteres.")
            .Matches("^[0-9]+$").WithMessage("CNPJ deve conter apenas números.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Length(11).WithMessage("Telefone deve conter 11 caracteres.")
            .Matches("^[0-9]+$").WithMessage("Telefone deve conter apenas números.");
    }
}

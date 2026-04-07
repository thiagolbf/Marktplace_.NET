using FluentValidation;

namespace Markplace.Application.DTOs.EnderecoContracts.Validator;

public class EnderecoDTOValidator : AbstractValidator<EnderecoDTO>
{
    public EnderecoDTOValidator()
    {
        RuleFor(x => x.Rua)
            .NotEmpty().WithMessage("Rua e obrigatoria.")
            .MaximumLength(50).WithMessage("Rua deve ter no maximo 50 caracteres.");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Numero e obrigatorio.")
            .MaximumLength(10).WithMessage("Numero deve ter no maximo 10 caracteres.");

        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage("Bairro e obrigatorio.")
            .MaximumLength(100).WithMessage("Bairro deve ter no maximo 100 caracteres.");

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage("Cidade e obrigatoria.")
            .MaximumLength(100).WithMessage("Cidade deve ter no maximo 100 caracteres.");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("Estado e obrigatorio.");

        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("CEP e obrigatorio.")
            .Length(9).WithMessage("CEP deve conter 9 caracteres.");
    }
}

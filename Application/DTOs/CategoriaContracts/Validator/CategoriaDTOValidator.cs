using FluentValidation;

namespace Markplace.Application.DTOs.CategoriaContracts.Validator;

public class CategoriaDTOValidator : AbstractValidator<CategoriaDTO>
{
    public CategoriaDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
                .WithMessage("O nome da categoria não pode estar vazio.")
            .MaximumLength(200)
                .WithMessage("O nome da categoria deve ter no máximo 200 caracteres.");
    }
}

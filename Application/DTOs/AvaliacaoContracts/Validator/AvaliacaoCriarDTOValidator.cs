using FluentValidation;

namespace Markplace.Application.DTOs.AvaliacaoContracts.Validator;

public class AvaliacaoCriarDTOValidator : AbstractValidator<AvaliacaoCriarDTO>
{
    public AvaliacaoCriarDTOValidator()
    {
        RuleFor(x => x.ProdutoId)
            .GreaterThan(0).WithMessage("ProdutoId invalido.");

        RuleFor(x => x.Nota)
            .InclusiveBetween(1, 5).WithMessage("Nota deve estar entre 1 e 5.");

        RuleFor(x => x.Comentario)
            .NotEmpty().WithMessage("Comentario e obrigatorio.")
            .MaximumLength(1000).WithMessage("Comentario deve ter no maximo 1000 caracteres.");
    }
}

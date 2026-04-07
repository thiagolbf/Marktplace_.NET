using FluentValidation;

namespace Markplace.Application.DTOs.AvaliacaoContracts.Validator;

public class AvaliacaoAtualizarDTOValidator : AbstractValidator<AvaliacaoAtualizarDTO>
{
    public AvaliacaoAtualizarDTOValidator()
    {
        RuleFor(x => x.Nota)
            .InclusiveBetween(1, 5).WithMessage("Nota deve estar entre 1 e 5.");

        RuleFor(x => x.Comentario)
            .NotEmpty().WithMessage("Comentario e obrigatorio.")
            .MaximumLength(1000).WithMessage("Comentario deve ter no maximo 1000 caracteres.");
    }
}

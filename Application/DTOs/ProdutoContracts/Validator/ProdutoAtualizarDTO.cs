using FluentValidation;

namespace Markplace.Application.DTOs.ProdutoContracts.Validator;

public class ProdutoAtualizarDTOValidator : AbstractValidator<ProdutoAtualizarDTO>
{
    public ProdutoAtualizarDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(100);
    }
}

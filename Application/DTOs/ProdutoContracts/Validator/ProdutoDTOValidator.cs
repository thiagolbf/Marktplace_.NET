using FluentValidation;

namespace Markplace.Application.DTOs.ProdutoContracts.Validator;

public class ProdutoDTOValidator : AbstractValidator<ProdutoDTO>
{
    public ProdutoDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Preco)
            .GreaterThan(0);

        RuleFor(x => x.Quantidade)
            .GreaterThan(0);

    }
}

using FluentValidation;

namespace Markplace.Application.DTOs.ProdutoContracts.Validator;

public class ProdutoAtualizarPrecoDTOValidator : AbstractValidator<ProdutoAtualizarPrecoDTO>
{

    public ProdutoAtualizarPrecoDTOValidator()
    {
        RuleFor(x => x.Preco)
            .GreaterThan(0)
            .WithMessage("O preço deve ser maior que zero.");
    }

}

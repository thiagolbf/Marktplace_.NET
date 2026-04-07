namespace Markplace.Application.DTOs.ProdutoContracts;

public record ProdutoResponseDTO(
    int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    string VendedorNome,
    IReadOnlyCollection<int> CategoriasIds
);

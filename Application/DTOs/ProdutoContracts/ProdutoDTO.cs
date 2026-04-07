
namespace Markplace.Application.DTOs.ProdutoContracts;


public record ProdutoDTO(
    string Nome,
    string Descricao,
    decimal Preco,
    int Quantidade,
    int VendedorId,
    //string Vendedor,
    IReadOnlyCollection<int> CategoriasIds
)
{ }

//public record ProdutoDTO
//{
//    public string Nome { get; init; } = string.Empty;
//    public string Descricao { get; init; } = default!;
//    public decimal Preco { get; init; }
//    public int Quantidade { get; init; }
//    public int VendedorId { get; init; }
//    public IReadOnlyCollection<int> CategoriasIds { get; init; }
//     = Array.Empty<int>();
//}

//public ProdutoDTO(string? nome, string? descricao, decimal preco, int quantidade, string? empresa, List<int> list)
//{
//    Nome = nome;
//    Descricao = descricao;
//    Preco = preco;
//    Quantidade = quantidade;
//}



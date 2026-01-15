using System.ComponentModel.DataAnnotations;

namespace Markplace.Application.DTOs;

public class ProdutoDTO
{
    [Required]
    [StringLength(200, ErrorMessage = "Nome é obrigatório")]
    public string? Nome { get; set; }

    [Required]
    [StringLength(250, ErrorMessage = "Descrição é obrigatório")]
    public string? Descricao { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que 0")]
    public decimal Preco { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que 0")]
    public int Quantidade { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "VendedorId é obrigatório")]
    public int VendedorId { get; set; }
}

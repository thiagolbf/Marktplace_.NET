using Markplace.Application.DTOs.AvaliacaoContracts;

namespace Markplace.Application.DTOs.ProdutoContracts
{
    public class ProdutoAdminDTO
    {
        public int Id { get; init; }
        public string Nome { get; init; } = null!;

        public string Empresa { get; init; } = null!;
        public string? Descricao { get; init; }

        public decimal Preco { get; init; }
        public int Quantidade { get; init; }

        public bool Ativo { get; init; }

        public int VendedorId { get; init; }
        public string VendedorEmpresa { get; init; } = null!;

        public IReadOnlyCollection<string> Categorias { get; init; }
            = Array.Empty<string>();

        public IReadOnlyCollection<AvaliacaoDTO> Avaliacoes { get; init; }
            = Array.Empty<AvaliacaoDTO>();
    }
}

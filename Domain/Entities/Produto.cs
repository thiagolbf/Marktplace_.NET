namespace Markplace.Domain.Entities;

public class Produto : Entity
{
    public string? Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para Produtor
    public int ProdutorId { get; private set; }
    //Propriedade navegação
    public Produtor? Produtor { get; private set; }
    public ICollection<ProdutoCategoria>? ProdutoCategorias { get; private set; }
    public ICollection<PedidoItem>? PedidoItens { get; private set; }
    public ICollection<Avaliacao>? Avaliacaos { get; private set; }

}

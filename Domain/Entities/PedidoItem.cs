namespace Markplace.Domain.Entities;

public class PedidoItem : Entity
{    
    public int Quantidade { get; private set; }
    public decimal Total { get; private set; }
    public decimal Preco { get; private set; }

    //FK para Pedido e Produto
    public int PedidoId { get; private set; }
    public int ProdutoId { get; private set; }

    // Propriedade de navegação
    public Pedido? Pedido { get; private set; }
    public Produto? Produto { get; private set; }
}

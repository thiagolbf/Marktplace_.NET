using Markplace.Domain.Enums;
using System.Numerics;

namespace Markplace.Domain.Entities;

public class Pedido : Entity
{
    public StatusPedido Status { get; private set; }
    public decimal ValorTotal { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para Cliente
    public int ClientId { get; private set; }

    //Propriedade de navegação
    public Cliente? Cliente { get; private set; }
    public Pagamento? Pagamento { get; private set; }
    public ICollection<PedidoItem>? PedidoItens {get; private set;}
}

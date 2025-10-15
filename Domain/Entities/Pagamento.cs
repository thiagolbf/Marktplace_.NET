using Markplace.Domain.Enums;

namespace Markplace.Domain.Entities;

public class Pagamento : Entity
{
    public MetodoPagamento MetodoPagamento { get; private set; }
    public decimal Total { get; private set; }
    public StatusPagamento StatusPagamento { get; private set; }
    public string? TransacaoId { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para Pedido
    public int PedidoId { get; private set; }

    //Propriedade navegação
    public Pedido? Pedido { get; private set; }

}

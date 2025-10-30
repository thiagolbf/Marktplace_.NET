namespace Markplace.Domain.Entities;

public class Avaliacao : Entity
{
    public int Nota { get; private set; } // de 1 a 5

    public string? Comentario { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK Cliente e Produto
    public int ClienteId { get; private set; }
    public int ProdutoId { get; private set; }

    //Propriedade navegação
    public Cliente? Cliente { get; private set; }
    public Produto? Produto { get; private set; }
}

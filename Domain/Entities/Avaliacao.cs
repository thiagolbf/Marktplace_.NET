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

    protected Avaliacao() { }

    public Avaliacao(int nota, string comentario, int clienteId, int produtoId)
    {
        if (nota < 1 || nota > 5)
            throw new Exception("Nota deve estar entre 1 e 5.");
        if (string.IsNullOrWhiteSpace(comentario))
            throw new Exception("Comentario e obrigatorio.");
        if (clienteId <= 0)
            throw new Exception("Cliente invalido.");
        if (produtoId <= 0)
            throw new Exception("Produto invalido.");

        Nota = nota;
        Comentario = comentario;
        ClienteId = clienteId;
        ProdutoId = produtoId;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void Atualizar(int nota, string comentario)
    {
        if (nota < 1 || nota > 5)
            throw new Exception("Nota deve estar entre 1 e 5.");
        if (string.IsNullOrWhiteSpace(comentario))
            throw new Exception("Comentario e obrigatorio.");

        Nota = nota;
        Comentario = comentario;
        AtualizadoEm = DateTime.UtcNow;
    }
}

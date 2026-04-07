namespace Markplace.Domain.Entities;

public class Categoria : Entity
{
    public string? Nome { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? AtualizadoEm { get; private set; }

    //Propriedade navegação
    public ICollection<ProdutoCategoria>? ProdutoCategorias { get; private set; }

    protected Categoria() { }

    public Categoria(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome é obrigatório.");

        Nome = nome;
        CriadoEm = DateTime.UtcNow;
    }

    public void Atualizar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome é obrigatório.");

        Nome = nome;
        AtualizadoEm = DateTime.UtcNow;
    }

}

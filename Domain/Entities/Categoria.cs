namespace Markplace.Domain.Entities;

public class Categoria : Entity
{
    public string? Nome { get; private set; }
    public bool Ativo   { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //Propriedade navegação
    public ICollection<ProdutoCategoria>? ProdutoCategorias { get; private set; }
}

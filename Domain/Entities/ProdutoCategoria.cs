namespace Markplace.Domain.Entities;

public class ProdutoCategoria
{
    //FK para identity
    public int ProdutoId { get; private set; }
    public int CategoriaId { get; private set; }

    //Propriedade navegação
    public Produto? Produto { get; private set; }
    public Cliente? Cliente { get; private set; }
}

namespace Markplace.Domain.Entities;

public class Produto : Entity
{
    public string? Nome { get; private set; }
    public string? Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int Quantidade { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? AtualizadoEm { get; private set; }

    //FK para Vendedor
    public int VendedorId { get; private set; }

    //Propriedade navegação
    public Vendedor Vendedor { get; private set; } = null!;
    public ICollection<ProdutoCategoria> ProdutoCategorias { get; private set; } = new List<ProdutoCategoria>();

    protected Produto() { }

    public Produto(string? nome, string? descricao, decimal preco, int quantidade, int vendedorId)
    {
        if (quantidade <= 0)
            throw new Exception("Produto não pode ser cadastrado com quantidade zero.");

        if (preco <= 0)
            throw new Exception("Produto não pode ser cadastrado com preço zero.");

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
        VendedorId = vendedorId;

        Ativo = true;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = null;

        ProdutoCategorias = new List<ProdutoCategoria>();
    }
    public void AdicionarCategorias(IEnumerable<int> categoriasIds)
    {
        foreach (var categoriaId in categoriasIds)
        {
            AdicionarCategoria(categoriaId);
        }
    }

    public void AdicionarCategoria(int categoriaId)
    {
        if (categoriaId <= 0)
            throw new Exception("Categoria inválida.");

        if (ProdutoCategorias.Any(pc => pc.CategoriaId == categoriaId))
            throw new Exception("Categoria já vinculada ao produto.");

        ProdutoCategorias.Add(new ProdutoCategoria(this, categoriaId));
    }

    public void Desativar()
    {
        if (!Ativo)
            throw new Exception("Produto já está desativado.");
            // TODO - Adicionar DomainException 

        Ativo = false;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarDados(string? nome, string? descricao)
    {
        if (!Ativo)
            throw new Exception("Não é possível atualizar produto desativado.");

        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome do produto é obrigatório.");

        Nome = nome;
        Descricao = descricao;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarPreco(decimal novoPreco)
    {
        if (!Ativo)
            throw new Exception("Não é possível alterar preço de produto desativado.");

        if (novoPreco <= 0)
            throw new Exception("Preço deve ser maior que zero.");

        Preco = novoPreco;
        AtualizadoEm = DateTime.UtcNow;
    }

}




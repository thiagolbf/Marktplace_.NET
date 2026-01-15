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
    public Vendedor? Vendedor { get; private set; }
    public ICollection<ProdutoCategoria>? ProdutoCategorias { get; private set; }
    public ICollection<PedidoItem>? PedidoItens { get; private set; }
    public ICollection<Avaliacao>? Avaliacoes { get; private set; }

    protected Produto() { }

    public Produto(string? nome, string? descricao, decimal preco, int quantidade, int vendedorId)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Quantidade = quantidade;
        VendedorId = vendedorId;

        Ativo = true;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = null;
    }

    public void Desativar()
    {
        if (Ativo)
        {
            // TODO - Adicionar DomainException 
        }

        Ativo = false;
        AtualizadoEm = DateTime.UtcNow;
    }

    //Produto AtualizarDTO!?
    public void Atualizar()
    {
        

    }
}





using Microsoft.AspNetCore.Identity;

namespace Markplace.Domain.Entities;

public class Produtor : Entity
{
    public string? Empresa { get; private set; }
    public string? Descricao { get; private set; }
    public string? CNPJ { get; private set; }
    public string? Telefone { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para identity
    public string? UserId { get; private set; }

    //Propriedade de navegação
    public IdentityUser? User { get; private set; }

    public ICollection<Produto>? Produtos { get; private set; }
}

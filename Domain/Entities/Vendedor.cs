using Microsoft.AspNetCore.Identity;

namespace Markplace.Domain.Entities;

public class Vendedor : Entity
{
    public string? Empresa { get; private set; }
    public string? Descricao { get; private set; }
    public string? CNPJ { get; private set; }
    public string? Telefone { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para identity
    public string? ApplicationUserId { get; private set; }

    //Propriedade de navegação
    public ApplicationUser? ApplicationUser { get; private set; }
    public ICollection<Produto>? Produtos { get; private set; }

    protected Vendedor() { }

    public Vendedor(string? empresa, string descricao, string cnpj, string telefone, string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new Exception("Descrição é obrigatória.");

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new Exception("CNPJ é obrigatório.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Telefone é obrigatório.");

        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new Exception("Usuário é obrigatório.");

        Empresa = empresa;
        Descricao = descricao;
        CNPJ = cnpj;
        Telefone = telefone;
        ApplicationUserId = applicationUserId;
        Ativo = true;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarPerfil(string? nome, string? telefone)
    {
        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Informe nome e/ou telefone para atualizar.");

        if (!string.IsNullOrWhiteSpace(nome))
            Descricao = nome;

        if (!string.IsNullOrWhiteSpace(telefone))
            Telefone = telefone;

        AtualizadoEm = DateTime.UtcNow;
    }

    public bool AlternarStatusAtivo()
    {
        Ativo = !Ativo;
        AtualizadoEm = DateTime.UtcNow;
        return Ativo;
    }
}

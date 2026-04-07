using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Markplace.Domain.Entities;

public class Cliente : Entity
{
    public string? Nome { get; private set; }
    public string? CPF { get; private set; }
    public string? Telefone { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para identity
    public string? ApplicationUserId { get; private set; }

    //Propriedade de navegacao
    public ApplicationUser? ApplicationUser { get; private set; }
    public ICollection<Endereco> Enderecos { get; private set; } = new List<Endereco>();
    public ICollection<Avaliacao>? Avaliacoes { get; private set; }

    protected Cliente() { }

    public Cliente(string nome, string cpf, string telefone, string applicationUserId)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new Exception("Nome e obrigatorio.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new Exception("CPF e obrigatorio.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Telefone e obrigatorio.");

        if (string.IsNullOrWhiteSpace(applicationUserId))
            throw new Exception("Usuario e obrigatorio.");

        Nome = nome;
        CPF = cpf;
        Telefone = telefone;
        ApplicationUserId = applicationUserId;
        Ativo = true;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void AtualizarPerfil(string? nome, string? cpf, string? telefone)
    {
        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cpf) && string.IsNullOrWhiteSpace(telefone))
            throw new Exception("Informe nome, cpf e/ou telefone para atualizar.");

        if (!string.IsNullOrWhiteSpace(nome))
            Nome = nome;

        if (!string.IsNullOrWhiteSpace(cpf))
            CPF = cpf;

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

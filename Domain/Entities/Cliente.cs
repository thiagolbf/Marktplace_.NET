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
        //public string? UserId { get; private set; }
        public string? ApplicationUserId { get; private set; }

        //Propriedade de navegação
        //public ApplicationUser? User { get; private set; }
        public ApplicationUser? ApplicationUser { get; private set; }
        public ICollection<Pedido>? Pedidos { get; private set; }
        public ICollection<Endereco> Enderecos { get; private set; } = new List<Endereco>();
        public ICollection<Avaliacao>? Avaliacoes { get; private set; }
    }

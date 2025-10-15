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
        public string? UserId {get; private set;}

        //Propriedade de navegação
        public IdentityUser? User { get; private set; }
        public ICollection<Pedido>? Pedidos { get; private set; }
        public ICollection<Endereco>? Enderecos { get; private set; }
        public ICollection<Avaliacao>? Avaliacaos { get; private set; }
    }

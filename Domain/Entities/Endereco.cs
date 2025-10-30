using Markplace.Domain.ValueObjects;

namespace Markplace.Domain.Entities;

public class Endereco : Entity
{    
    public EnderecoValor? EnderecoValor { get; private set; }    
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    //FK para Cliente
    public int ClienteId { get; private set; }
    
    // Propriedade de navegação
    public Cliente? Cliente { get; private set; }
}

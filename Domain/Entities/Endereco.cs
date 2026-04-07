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

    protected Endereco() { }

    public Endereco(int clienteId, EnderecoValor enderecoValor)
    {
        if (clienteId <= 0)
            throw new Exception("Cliente invalido.");
        EnderecoValor = enderecoValor ?? throw new ArgumentNullException(nameof(enderecoValor));
        ClienteId = clienteId;
        CriadoEm = DateTime.UtcNow;
        AtualizadoEm = DateTime.UtcNow;
    }

    public void Atualizar(EnderecoValor enderecoValor)
    {
        EnderecoValor = enderecoValor ?? throw new ArgumentNullException(nameof(enderecoValor));
        AtualizadoEm = DateTime.UtcNow;
    }
}

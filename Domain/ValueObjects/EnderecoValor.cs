namespace Markplace.Domain.ValueObjects;

public class EnderecoValor
{
    public string? Rua { get; private set; }
    public string? Numero { get; private set; }
    public string? Bairro { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? Cep { get; private set; }

    public override bool Equals(object? obj)
    {
        if (obj is not EnderecoValor other) return false;
        return Rua == other.Rua &&
            Numero == other.Numero &&
            Bairro == other.Bairro &&
            Cidade == other.Cidade &&
            Estado == other.Estado &&
            Cep == other.Cep;
    }

    public override int GetHashCode() => HashCode.Combine(Rua, Numero, Cidade, Estado, Cep);

}

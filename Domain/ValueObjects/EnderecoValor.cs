namespace Markplace.Domain.ValueObjects;

public class EnderecoValor
{
    public string? Rua { get; private set; }
    public string? Numero { get; private set; }
    public string? Bairro { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? Cep { get; private set; }

    protected EnderecoValor() { }

    public EnderecoValor(string rua, string numero, string bairro, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(rua))
            throw new Exception("Rua e obrigatoria.");
        if (string.IsNullOrWhiteSpace(numero))
            throw new Exception("Numero e obrigatorio.");
        if (string.IsNullOrWhiteSpace(bairro))
            throw new Exception("Bairro e obrigatorio.");
        if (string.IsNullOrWhiteSpace(cidade))
            throw new Exception("Cidade e obrigatoria.");
        if (string.IsNullOrWhiteSpace(estado))
            throw new Exception("Estado e obrigatorio.");
        if (string.IsNullOrWhiteSpace(cep))
            throw new Exception("CEP e obrigatorio.");

        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
    }

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

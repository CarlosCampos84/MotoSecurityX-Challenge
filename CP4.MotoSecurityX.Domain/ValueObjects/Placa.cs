namespace CP4.MotoSecurityX.Domain.ValueObjects;

public sealed class Placa : IEquatable<Placa>
{
    public string Value { get; }

    private Placa(string value) => Value = value;

    public static Placa Create(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new ArgumentException("Placa não pode ser vazia.");

        var norm = raw.Trim().ToUpperInvariant().Replace("-", "");
        if (norm.Length is < 7 or > 8) // ajuste conforme regra que decidir usar
            throw new ArgumentException("Placa em formato inválido.");

        return new Placa(norm);
    }

    public override string ToString() => Value;

    public bool Equals(Placa? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Placa p && Equals(p);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
}
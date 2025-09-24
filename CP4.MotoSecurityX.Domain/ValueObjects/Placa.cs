namespace CP4.MotoSecurityX.Domain.ValueObjects;

public sealed class Placa : IEquatable<Placa>
{
    public string Value { get; }

    // Construtor PÚBLICO com validação
    public Placa(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw new ArgumentException("Placa não pode ser vazia.", nameof(raw));

        var norm = raw.Trim().ToUpperInvariant().Replace("-", "");
        if (norm.Length is < 7 or > 8) // ajuste se quiser ser mais estrito
            throw new ArgumentException("Placa em formato inválido.", nameof(raw));

        Value = norm;
    }

    // Factory opcional
    public static Placa Create(string raw) => new Placa(raw);

    public override string ToString() => Value;

    public bool Equals(Placa? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Placa p && Equals(p);
    public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);
}


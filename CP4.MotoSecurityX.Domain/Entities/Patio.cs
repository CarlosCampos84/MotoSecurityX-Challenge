namespace CP4.MotoSecurityX.Domain.Entities;

public class Patio
{
    private readonly List<Moto> _motos = new();
    private Patio() { } // EF

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; } = "";
    public string Endereco { get; private set; } = "";

    public IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

    public Patio(string nome, string endereco)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
        if (string.IsNullOrWhiteSpace(endereco)) throw new ArgumentException("Endereço inválido");
        Nome = nome.Trim();
        Endereco = endereco.Trim();
    }

    public void AdmitirMoto(Moto moto)
    {
        if (moto is null) throw new ArgumentNullException(nameof(moto));
        if (_motos.Any(m => m.Id == moto.Id)) return;

        moto.EntrarNoPatio(Id);
        _motos.Add(moto);
    }

    public void RemoverMoto(Moto moto)
    {
        if (moto is null) throw new ArgumentNullException(nameof(moto));
        if (_motos.RemoveAll(m => m.Id == moto.Id) > 0)
            moto.SairDoPatio();
    }
}
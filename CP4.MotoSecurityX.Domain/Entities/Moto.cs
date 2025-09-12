using CP4.MotoSecurityX.Domain.ValueObjects;

namespace CP4.MotoSecurityX.Domain.Entities;

public class Moto
{
    private Moto() { } // EF

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Placa Placa { get; private set; } = null!;
    public string Modelo { get; private set; } = "";
    public bool DentroDoPatio { get; private set; }
    public Guid? PatioId { get; private set; }

    public Moto(Placa placa, string modelo)
    {
        Placa = placa ?? throw new ArgumentNullException(nameof(placa));
        AtualizarModelo(modelo);
        DentroDoPatio = false;
    }

    public void EntrarNoPatio(Guid patioId)
    {
        DentroDoPatio = true;
        PatioId = patioId;
    }

    public void SairDoPatio()
    {
        DentroDoPatio = false;
        PatioId = null;
    }

    public void AtualizarModelo(string novoModelo)
    {
        if (string.IsNullOrWhiteSpace(novoModelo)) throw new ArgumentException("Modelo inválido");
        Modelo = novoModelo.Trim();
    }
}
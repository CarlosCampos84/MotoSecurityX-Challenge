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

    /// <summary>
    /// Coloca a moto em um pátio, garantindo invariantes.
    /// </summary>
    public void EntrarNoPatio(Guid patioId)
    {
        if (patioId == Guid.Empty)
            throw new ArgumentException("PatioId inválido.", nameof(patioId));

        // idempotência: se já está no mesmo pátio, não faz nada
        if (DentroDoPatio && PatioId == patioId)
            return;

        PatioId = patioId;
        DentroDoPatio = true;
    }

    /// <summary>
    /// Remove a moto do pátio (se estiver).
    /// </summary>
    public void SairDoPatio()
    {
        // idempotência: se já está fora, não faz nada
        if (!DentroDoPatio && PatioId is null)
            return;

        PatioId = null;
        DentroDoPatio = false;
    }
    public void AtualizarModelo(string modelo)
    {
        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("Modelo inválido");
        Modelo = modelo.Trim();
    }

    public void AtualizarPlaca(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new ArgumentException("Placa inválida");
        Placa = new ValueObjects.Placa(placa.Trim());
    }

}
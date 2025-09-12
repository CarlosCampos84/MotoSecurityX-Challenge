using CP4.MotoSecurityX.Domain.Repositories;

namespace CP4.MotoSecurityX.Application.UseCases.Motos;

public sealed class MoveMotoToPatioHandler
{
    private readonly IMotoRepository _motoRepo;
    private readonly IPatioRepository _patioRepo;

    public MoveMotoToPatioHandler(IMotoRepository motoRepo, IPatioRepository patioRepo)
    {
        _motoRepo = motoRepo;
        _patioRepo = patioRepo;
    }

    public async Task<bool> HandleAsync(Guid motoId, Guid patioId, CancellationToken ct = default)
    {
        var moto = await _motoRepo.GetByIdAsync(motoId, ct);
        if (moto is null) return false;

        var patio = await _patioRepo.GetByIdAsync(patioId, ct);
        if (patio is null) return false;

        patio.AdmitirMoto(moto);
        await _patioRepo.UpdateAsync(patio, ct);
        await _motoRepo.UpdateAsync(moto, ct);
        return true;
    }
}
using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Domain.Repositories;

namespace CP4.MotoSecurityX.Application.UseCases.Motos;

public sealed class ListMotosHandler
{
    private readonly IMotoRepository _repo;
    public ListMotosHandler(IMotoRepository repo) => _repo = repo;

    public async Task<List<MotoDto>> HandleAsync(CancellationToken ct = default)
    {
        var list = await _repo.ListAsync(ct);
        return list.Select(m => new MotoDto(m.Id, m.Placa.Value, m.Modelo, m.DentroDoPatio, m.PatioId)).ToList();
    }
}
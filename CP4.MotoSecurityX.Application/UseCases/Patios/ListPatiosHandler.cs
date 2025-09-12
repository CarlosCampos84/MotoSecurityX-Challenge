using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Domain.Repositories;

namespace CP4.MotoSecurityX.Application.UseCases.Patios;

public sealed class ListPatiosHandler
{
    private readonly IPatioRepository _repo;
    public ListPatiosHandler(IPatioRepository repo) => _repo = repo;

    public async Task<List<PatioDto>> HandleAsync(CancellationToken ct = default)
    {
        var list = await _repo.ListAsync(ct);
        return list.Select(p => new PatioDto(p.Id, p.Nome, p.Endereco, p.Motos.Count)).ToList();
    }
}
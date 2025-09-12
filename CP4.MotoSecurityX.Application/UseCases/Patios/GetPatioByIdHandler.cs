using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Domain.Repositories;

namespace CP4.MotoSecurityX.Application.UseCases.Patios;

public sealed class GetPatioByIdHandler
{
    private readonly IPatioRepository _repo;
    public GetPatioByIdHandler(IPatioRepository repo) => _repo = repo;

    public async Task<PatioDto?> HandleAsync(Guid id, CancellationToken ct = default)
    {
        var p = await _repo.GetByIdAsync(id, ct);
        return p is null ? null : new PatioDto(p.Id, p.Nome, p.Endereco, p.Motos.Count);
    }
}
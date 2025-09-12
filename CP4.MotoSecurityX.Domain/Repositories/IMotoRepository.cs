using CP4.MotoSecurityX.Domain.Entities;

namespace CP4.MotoSecurityX.Domain.Repositories;

public interface IMotoRepository
{
    Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Moto?> GetByPlacaAsync(string placa, CancellationToken ct = default);
    Task<List<Moto>> ListAsync(CancellationToken ct = default);
    Task AddAsync(Moto moto, CancellationToken ct = default);
    Task UpdateAsync(Moto moto, CancellationToken ct = default);
    Task DeleteAsync(Moto moto, CancellationToken ct = default);
}
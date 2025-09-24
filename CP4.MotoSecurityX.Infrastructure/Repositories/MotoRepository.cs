using CP4.MotoSecurityX.Domain.Entities;
using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Repositories;

public sealed class MotoRepository : IMotoRepository
{
    private readonly AppDbContext _db;
    public MotoRepository(AppDbContext db) => _db = db;

    public async Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Motos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task AddAsync(Moto moto, CancellationToken ct = default)
    {
        _db.Motos.Add(moto);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Moto moto, CancellationToken ct = default)
    {
        _db.Motos.Update(moto);
        await _db.SaveChangesAsync(ct);
    }

    // novos
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.Motos.FirstOrDefaultAsync(m => m.Id == id, ct);
        if (entity is null) return false;
        _db.Motos.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public Task<int> CountAsync(CancellationToken ct = default)
        => _db.Motos.CountAsync(ct);

    public async Task<IReadOnlyList<Moto>> ListAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var skip = (page - 1) * pageSize;
        return await _db.Motos
            .AsNoTracking()
            .OrderBy(m => m.Modelo)
            .ThenBy(m => m.Placa.Value)   // Placa é VO
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }
}
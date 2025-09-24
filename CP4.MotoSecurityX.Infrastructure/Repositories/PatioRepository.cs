using CP4.MotoSecurityX.Domain.Entities;
using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Repositories;

public sealed class PatioRepository : IPatioRepository
{
    private readonly AppDbContext _db;
    public PatioRepository(AppDbContext db) => _db = db;

    public async Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Patios.AsNoTracking()
            .Include(p => p.Motos)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task AddAsync(Patio patio, CancellationToken ct = default)
    {
        _db.Patios.Add(patio);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Patio patio, CancellationToken ct = default)
    {
        _db.Patios.Update(patio);
        await _db.SaveChangesAsync(ct);
    }

    // novos
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.Patios.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return false;
        _db.Patios.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public Task<int> CountAsync(CancellationToken ct = default)
        => _db.Patios.CountAsync(ct);

    public async Task<IReadOnlyList<Patio>> ListAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var skip = (page - 1) * pageSize;
        return await _db.Patios
            .AsNoTracking()
            .Include(p => p.Motos) // para ter a contagem no handler
            .OrderBy(p => p.Nome)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }
}
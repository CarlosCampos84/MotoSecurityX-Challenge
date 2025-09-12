using CP4.MotoSecurityX.Domain.Entities;
using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Repositories;

public class EfPatioRepository : IPatioRepository
{
    private readonly AppDbContext _db;
    public EfPatioRepository(AppDbContext db) => _db = db;

    public Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Patios
            .Include(p => p.Motos)        // sua modelagem já usa p.Motos
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<List<Patio>> ListAsync(CancellationToken ct = default) =>
        _db.Patios
            .Include(p => p.Motos)
            .AsNoTracking()
            .ToListAsync(ct);

    public async Task AddAsync(Patio patio, CancellationToken ct = default)
    {
        await _db.Patios.AddAsync(patio, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Patio patio, CancellationToken ct = default)
    {
        _db.Patios.Update(patio);
        await _db.SaveChangesAsync(ct);
    }
}
using CP4.MotoSecurityX.Domain.Entities;
using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Repositories;

public class EfMotoRepository : IMotoRepository
{
    private readonly AppDbContext _db;
    public EfMotoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Moto moto, CancellationToken ct = default)
    {
        await _db.Motos.AddAsync(moto, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Moto moto, CancellationToken ct = default)
    {
        _db.Motos.Update(moto);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Moto moto, CancellationToken ct = default)
    {
        _db.Motos.Remove(moto);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Motos.FirstOrDefaultAsync(m => m.Id == id, ct);

    public Task<Moto?> GetByPlacaAsync(string placa, CancellationToken ct = default) =>
        _db.Motos.FirstOrDefaultAsync(m => m.Placa.Value == placa.ToUpper(), ct);
    
    public Task<List<Moto>> ListAsync(CancellationToken ct = default) =>
        _db.Motos.AsNoTracking().ToListAsync(ct);
}
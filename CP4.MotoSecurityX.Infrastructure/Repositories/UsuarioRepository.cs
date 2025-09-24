using CP4.MotoSecurityX.Domain.Entities;
using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CP4.MotoSecurityX.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;
    public UsuarioRepository(AppDbContext db) => _db = db;

    public async Task<Usuario?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default) =>
        await _db.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);

    public async Task<List<Usuario>> ListAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var skip = (page - 1) * pageSize;
        return await _db.Usuarios
            .AsNoTracking()
            .OrderBy(u => u.Nome)
            .ThenBy(u => u.Email)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);
    }

    public Task<int> CountAsync(CancellationToken ct = default) =>
        _db.Usuarios.CountAsync(ct);

    public async Task AddAsync(Usuario usuario, CancellationToken ct = default)
    {
        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Usuario usuario, CancellationToken ct = default)
    {
        _db.Usuarios.Update(usuario);
        await _db.SaveChangesAsync(ct);
    }

    // <- agora retorna Task (sem bool), conforme a interface
    public async Task DeleteAsync(Usuario usuario, CancellationToken ct = default)
    {
        if (usuario is null) return;

        // se vier desanexado, anexa antes de remover
        if (_db.Entry(usuario).State == EntityState.Detached)
            _db.Usuarios.Attach(usuario);

        _db.Usuarios.Remove(usuario);
        await _db.SaveChangesAsync(ct);
    }
}


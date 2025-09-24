using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Domain.Repositories;

namespace CP4.MotoSecurityX.Application.UseCases.Usuarios;

public class GetUsuarioByIdHandler
{
    private readonly IUsuarioRepository _repo;
    public GetUsuarioByIdHandler(IUsuarioRepository repo) => _repo = repo;

    public async Task<UsuarioDto?> HandleAsync(Guid id, CancellationToken ct = default)
    {
        var u = await _repo.GetByIdAsync(id, ct);
        return u is null ? null : new UsuarioDto(u.Id, u.Nome, u.Email);
    }
}
namespace CP4.MotoSecurityX.Application.DTOs;

public record CreateUsuarioDto(string Nome, string Email);
public record UpdateUsuarioDto(string Nome, string Email);
public record UsuarioDto(Guid Id, string Nome, string Email);
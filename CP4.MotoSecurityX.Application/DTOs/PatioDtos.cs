namespace CP4.MotoSecurityX.Application.DTOs;

public record PatioDto(Guid Id, string Nome, string Endereco, int QuantidadeMotos);
public record CreatePatioDto(string Nome, string Endereco);
public record UpdatePatioDto(string Nome, string Endereco);
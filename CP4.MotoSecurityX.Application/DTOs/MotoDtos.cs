namespace CP4.MotoSecurityX.Application.DTOs;

public record MotoDto(Guid Id, string Placa, string Modelo, bool DentroDoPatio, Guid? PatioId);
public record CreateMotoDto(string Placa, string Modelo);
public record UpdateMotoDto(string Modelo);
public record MoveMotoDto(Guid PatioId);
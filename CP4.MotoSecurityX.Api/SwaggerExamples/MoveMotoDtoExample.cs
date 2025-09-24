using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para mover uma moto para um pátio no Swagger.
/// </summary>
public sealed class MoveMotoDtoExample : IExamplesProvider<MoveMotoDto>
{
    public MoveMotoDto GetExamples()
        // Coloque aqui um GUID de exemplo legível; o valor real será substituído no teste
        => new MoveMotoDto(Guid.Parse("11111111-2222-3333-4444-555555555555"));
}
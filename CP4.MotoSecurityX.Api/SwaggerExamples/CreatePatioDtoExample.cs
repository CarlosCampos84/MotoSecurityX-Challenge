using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para criação de Pátio no Swagger.
/// </summary>
public sealed class CreatePatioDtoExample : IExamplesProvider<CreatePatioDto>
{
    public CreatePatioDto GetExamples() =>
        new("Pátio Central", "Rua das Entregas, 100");
}
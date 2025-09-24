using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para atualização de Pátio no Swagger.
/// </summary>
public sealed class UpdatePatioDtoExample : IExamplesProvider<UpdatePatioDto>
{
    public UpdatePatioDto GetExamples() =>
        new("Pátio Norte Atualizado", "Avenida Principal, 200");
}
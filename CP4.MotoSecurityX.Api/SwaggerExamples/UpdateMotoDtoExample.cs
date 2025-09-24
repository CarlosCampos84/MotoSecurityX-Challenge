using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para atualização de Moto no Swagger.
/// </summary>
public sealed class UpdateMotoDtoExample : IExamplesProvider<UpdateMotoDto>
{
    public UpdateMotoDto GetExamples() =>
        new("Mottu 125i", "XYZ9A88");
}
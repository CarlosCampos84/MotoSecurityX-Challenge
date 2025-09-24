using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para criação de Usuário no Swagger.
/// </summary>
public sealed class CreateUsuarioDtoExample : IExamplesProvider<CreateUsuarioDto>
{
    public CreateUsuarioDto GetExamples() =>
        new("Operador João", "joao@mottu.com");
}
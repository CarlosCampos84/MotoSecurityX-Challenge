using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

/// <summary>
/// Exemplo de payload para atualização de Usuário no Swagger.
/// </summary>
public sealed class UpdateUsuarioDtoExample : IExamplesProvider<UpdateUsuarioDto>
{
    public UpdateUsuarioDto GetExamples() =>
        new("Operador João Atualizado", "joao.atualizado@mottu.com");
}
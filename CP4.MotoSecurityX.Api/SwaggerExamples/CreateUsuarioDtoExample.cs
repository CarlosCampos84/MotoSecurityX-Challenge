using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

public class CreateUsuarioDtoExample : IExamplesProvider<CreateUsuarioDto>
{
    public CreateUsuarioDto GetExamples() => new("Operador João", "joao@mottu.com");
}
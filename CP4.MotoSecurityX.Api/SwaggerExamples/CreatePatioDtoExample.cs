using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

public class CreatePatioDtoExample : IExamplesProvider<CreatePatioDto>
{
    public CreatePatioDto GetExamples() => new("Pátio Central", "Rua 1");
}
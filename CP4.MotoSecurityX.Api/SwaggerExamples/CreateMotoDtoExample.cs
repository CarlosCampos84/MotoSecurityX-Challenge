using CP4.MotoSecurityX.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.SwaggerExamples;

public class CreateMotoDtoExample : IExamplesProvider<CreateMotoDto>
{
    public CreateMotoDto GetExamples() => new("ABC1D23", "Mottu 110i");
}
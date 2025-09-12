using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Application.UseCases.Patios;
using Microsoft.AspNetCore.Mvc;

namespace CP4.MotoSecurityX.Api.Controllers;

[ApiController]
[Route("api/patios")]
public class PatiosController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List([FromServices] ListPatiosHandler handler, CancellationToken ct)
        => Ok(await handler.HandleAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetPatioByIdHandler handler, CancellationToken ct)
    {
        var patio = await handler.HandleAsync(id, ct);
        return patio is null ? NotFound() : Ok(patio);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatioDto dto, [FromServices] CreatePatioHandler handler, CancellationToken ct)
    {
        var created = await handler.HandleAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Application.UseCases.Motos;
using Microsoft.AspNetCore.Mvc;

namespace CP4.MotoSecurityX.Api.Controllers;

[ApiController]
[Route("api/motos")]
public class MotosController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List([FromServices] ListMotosHandler handler, CancellationToken ct)
        => Ok(await handler.HandleAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetMotoByIdHandler handler, CancellationToken ct)
    {
        var moto = await handler.HandleAsync(id, ct);
        return moto is null ? NotFound() : Ok(moto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMotoDto dto, [FromServices] CreateMotoHandler handler, CancellationToken ct)
    {
        var created = await handler.HandleAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id:guid}/mover")]
    public async Task<IActionResult> Move(Guid id, [FromBody] MoveMotoDto dto, [FromServices] MoveMotoToPatioHandler handler, CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, dto.PatioId, ct);
        return ok ? NoContent() : NotFound();
    }
}
using CP4.MotoSecurityX.Application.Common;
using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Application.UseCases.Motos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using CP4.MotoSecurityX.Api.SwaggerExamples;

namespace CP4.MotoSecurityX.Api.Controllers;

[ApiController]
[Route("api/motos")]
public class MotosController : ControllerBase
{
    private string Link(int page, int size) =>
        Url.ActionLink(nameof(List), values: new { page, pageSize = size }) ?? string.Empty;

    // LIST paginada + HATEOAS
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<MotoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromServices] ListMotosHandler handler = null!,
        CancellationToken ct = default)
    {
        var result = await handler.HandleAsync(page, pageSize, Link, ct);
        return Ok(result);
    }

    // GET BY ID (opcional, mas útil para CreatedAtAction do POST)
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MotoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] GetMotoByIdHandler handler,
        CancellationToken ct)
    {
        var dto = await handler.HandleAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    // CREATE
    [HttpPost]
    [SwaggerOperation(Summary = "Cria uma moto")]
    [SwaggerRequestExample(typeof(CreateMotoDto), typeof(CreateMotoDtoExample))]
    [ProducesResponseType(typeof(MotoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        CreateMotoDto dto,
        [FromServices] CreateMotoHandler handler,
        CancellationToken ct)
    {
        var id = await handler.HandleAsync(dto, ct);
        // retorna 201 com Location para o GET by id
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    // MOVER MOTO PARA PÁTIO
    // (corrigido: usa MoveMotoDto – NÃO MoveMotoToPatioDto)
    [HttpPost("{id:guid}/mover")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Move(
        Guid id,
        MoveMotoDto dto,
        [FromServices] MoveMotoToPatioHandler handler,
        CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, dto.PatioId, ct);
        return ok ? NoContent() : NotFound();
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateMotoDto dto,
        [FromServices] UpdateMotoHandler handler,
        CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, dto, ct);
        return ok ? NoContent() : NotFound();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteMotoHandler handler,
        CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}

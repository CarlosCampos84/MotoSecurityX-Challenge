using CP4.MotoSecurityX.Application.Common;
using CP4.MotoSecurityX.Application.DTOs;
using CP4.MotoSecurityX.Application.UseCases.Patios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CP4.MotoSecurityX.Api.Controllers;

[ApiController]
[Route("api/patios")]
public class PatiosController : ControllerBase
{
    private string Link(int page, int size) =>
        Url.ActionLink(nameof(List), values: new { page, pageSize = size }) ?? string.Empty;

    // GET paginado + HATEOAS
    [HttpGet]
    [SwaggerOperation(Summary = "Lista pátios com paginação")]
    [ProducesResponseType(typeof(PagedResult<PatioDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromServices] ListPatiosHandler handler = null!,
        CancellationToken ct = default)
    {
        var result = await handler.HandleAsync(page, pageSize, Link, ct);
        return Ok(result);
    }

    // GET by id (preserva seu contrato)
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PatioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromServices] GetPatioByIdHandler handler,
        CancellationToken ct)
    {
        var dto = await handler.HandleAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    // POST (com exemplo no Swagger)
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um pátio")]
    [SwaggerRequestExample(typeof(CreatePatioDto), typeof(CP4.MotoSecurityX.Api.SwaggerExamples.CreatePatioDtoExample))]
    [ProducesResponseType(typeof(PatioDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        CreatePatioDto dto,
        [FromServices] CreatePatioHandler handler,
        CancellationToken ct)
    {
        var created = await handler.HandleAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT
    [HttpPut("{id:guid}")]
    [SwaggerOperation(Summary = "Atualiza um pátio")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        UpdatePatioDto dto,
        [FromServices] UpdatePatioHandler handler,
        CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, dto, ct);
        return ok ? NoContent() : NotFound();
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Exclui um pátio")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeletePatioHandler handler,
        CancellationToken ct)
    {
        var ok = await handler.HandleAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}

using Microsoft.AspNetCore.Mvc;
using TurnosMedicos.Api.Contracts;
using TurnosMedicos.Application.Turnos.CancelarTurno;
using TurnosMedicos.Application.Turnos.ObtenerTurno;
using TurnosMedicos.Application.Turnos.ReservarTurno;

namespace TurnosMedicos.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TurnosController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<TurnoReservadoResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Reservar(
        ReservarTurnoCommand command,
        [FromServices] ReservarTurnoHandler handler,
        CancellationToken cancellationToken)
    {
        var turnoId = await handler.HandleAsync(command, cancellationToken);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = turnoId }, new TurnoReservadoResponse(turnoId));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<TurnoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObtenerPorId(
        Guid id,
        [FromServices] ObtenerTurnoHandler handler,
        CancellationToken cancellationToken)
    {
        var turno = await handler.HandleAsync(new ObtenerTurnoQuery(id), cancellationToken);

        return turno is null ? NotFound() : Ok(turno);
    }

    [HttpPost("{id:guid}/cancelar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Cancelar(
        Guid id,
        [FromServices] CancelarTurnoHandler handler,
        CancellationToken cancellationToken)
    {
        var cancelado = await handler.HandleAsync(new CancelarTurnoCommand(id), cancellationToken);

        return cancelado ? NoContent() : NotFound();
    }
}

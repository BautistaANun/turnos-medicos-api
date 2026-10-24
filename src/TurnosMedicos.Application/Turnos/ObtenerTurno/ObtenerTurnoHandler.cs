using TurnosMedicos.Application.Abstractions;

namespace TurnosMedicos.Application.Turnos.ObtenerTurno;

public class ObtenerTurnoHandler(ITurnoRepository repository)
{
    public async Task<TurnoDto?> HandleAsync(ObtenerTurnoQuery query, CancellationToken cancellationToken = default)
    {
        var turno = await repository.ObtenerPorIdAsync(query.TurnoId, cancellationToken);

        if (turno is null)
        {
            return null;
        }

        return new TurnoDto(turno.Id, turno.AfiliadoId, turno.ProfesionalId, turno.Inicio, turno.Estado.ToString());
    }
}

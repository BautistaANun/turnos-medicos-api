using TurnosMedicos.Application.Abstractions;

namespace TurnosMedicos.Application.Turnos.CancelarTurno;

public class CancelarTurnoHandler(ITurnoRepository repository, TimeProvider timeProvider)
{
    /// <returns><c>false</c> si el turno no existe.</returns>
    public async Task<bool> HandleAsync(CancelarTurnoCommand command, CancellationToken cancellationToken = default)
    {
        var turno = await repository.ObtenerPorIdAsync(command.TurnoId, cancellationToken);

        if (turno is null)
        {
            return false;
        }

        turno.Cancelar(timeProvider.GetUtcNow().UtcDateTime);

        await repository.ActualizarAsync(turno, cancellationToken);

        return true;
    }
}

using TurnosMedicos.Application.Abstractions;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Application.Turnos.ReservarTurno;

public class ReservarTurnoHandler(ITurnoRepository repository, TimeProvider timeProvider)
{
    public async Task<Guid> HandleAsync(ReservarTurnoCommand command, CancellationToken cancellationToken = default)
    {
        var ahora = timeProvider.GetUtcNow().UtcDateTime;

        var turno = Turno.Reservar(command.AfiliadoId, command.ProfesionalId, command.Inicio, ahora);

        await repository.AgregarAsync(turno, cancellationToken);

        return turno.Id;
    }
}

using TurnosMedicos.Application.Abstractions;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Application.Tests.Fakes;

public class InMemoryTurnoRepository : ITurnoRepository
{
    public List<Turno> Turnos { get; } = [];

    public Task AgregarAsync(Turno turno, CancellationToken cancellationToken = default)
    {
        Turnos.Add(turno);
        return Task.CompletedTask;
    }
}

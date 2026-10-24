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

    public Task<Turno?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Turnos.FirstOrDefault(t => t.Id == id));
    }

    public Task ActualizarAsync(Turno turno, CancellationToken cancellationToken = default)
    {
        // La lista guarda la misma instancia, así que los cambios ya quedaron aplicados.
        return Task.CompletedTask;
    }
}

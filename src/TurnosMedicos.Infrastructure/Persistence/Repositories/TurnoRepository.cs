using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Application.Abstractions;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Infrastructure.Persistence.Repositories;

public class TurnoRepository(TurnosDbContext dbContext) : ITurnoRepository
{
    public async Task AgregarAsync(Turno turno, CancellationToken cancellationToken = default)
    {
        dbContext.Turnos.Add(turno);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<Turno?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Turnos.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task ActualizarAsync(Turno turno, CancellationToken cancellationToken = default)
    {
        // El turno se obtuvo con este mismo DbContext, así que EF ya detectó los cambios (change tracking).
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

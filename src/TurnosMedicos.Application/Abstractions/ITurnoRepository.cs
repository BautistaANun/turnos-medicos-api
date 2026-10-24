using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Application.Abstractions;

public interface ITurnoRepository
{
    Task AgregarAsync(Turno turno, CancellationToken cancellationToken = default);
}

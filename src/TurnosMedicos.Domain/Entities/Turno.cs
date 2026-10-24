using TurnosMedicos.Domain.Enums;

namespace TurnosMedicos.Domain.Entities;

public class Turno
{
    public Guid Id { get; private set; }
    public Guid AfiliadoId { get; private set; }
    public Guid ProfesionalId { get; private set; }
    public DateTime Inicio { get; private set; }
    public EstadoTurno Estado { get; private set; }

    private Turno() { }

    public static Turno Reservar(Guid afiliadoId, Guid profesionalId, DateTime inicio)
    {
        return new Turno
        {
            Id = Guid.NewGuid(),
            AfiliadoId = afiliadoId,
            ProfesionalId = profesionalId,
            Inicio = inicio,
            Estado = EstadoTurno.Reservado
        };
    }
}

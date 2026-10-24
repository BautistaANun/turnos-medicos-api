namespace TurnosMedicos.Application.Turnos.ReservarTurno;

/// <param name="Inicio">Fecha y hora de inicio del turno, en UTC.</param>
public record ReservarTurnoCommand(Guid AfiliadoId, Guid ProfesionalId, DateTime Inicio);

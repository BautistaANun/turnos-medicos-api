namespace TurnosMedicos.Application.Turnos.ObtenerTurno;

public record TurnoDto(Guid Id, Guid AfiliadoId, Guid ProfesionalId, DateTime Inicio, string Estado);

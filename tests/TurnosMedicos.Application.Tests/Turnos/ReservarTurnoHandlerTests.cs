using Microsoft.Extensions.Time.Testing;
using TurnosMedicos.Application.Tests.Fakes;
using TurnosMedicos.Application.Turnos.ReservarTurno;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Exceptions;

namespace TurnosMedicos.Application.Tests.Turnos;

public class ReservarTurnoHandlerTests
{
    private readonly InMemoryTurnoRepository _repository = new();
    private readonly FakeTimeProvider _timeProvider = new(new DateTimeOffset(2027, 3, 1, 9, 0, 0, TimeSpan.Zero));
    private readonly ReservarTurnoHandler _handler;

    public ReservarTurnoHandlerTests()
    {
        _handler = new ReservarTurnoHandler(_repository, _timeProvider);
    }

    [Fact]
    public async Task HandleAsync_ConDatosValidos_GuardaElTurnoReservado()
    {
        // Arrange
        var command = new ReservarTurnoCommand(
            AfiliadoId: Guid.NewGuid(),
            ProfesionalId: Guid.NewGuid(),
            Inicio: new DateTime(2027, 3, 15, 10, 0, 0, DateTimeKind.Utc));

        // Act
        var turnoId = await _handler.HandleAsync(command);

        // Assert
        var turno = Assert.Single(_repository.Turnos);
        Assert.Equal(turnoId, turno.Id);
        Assert.Equal(command.AfiliadoId, turno.AfiliadoId);
        Assert.Equal(command.ProfesionalId, turno.ProfesionalId);
        Assert.Equal(command.Inicio, turno.Inicio);
        Assert.Equal(EstadoTurno.Reservado, turno.Estado);
    }

    [Fact]
    public async Task HandleAsync_ConInicioAnteriorAlRelojDelSistema_LanzaDomainExceptionYNoGuarda()
    {
        // Arrange
        _timeProvider.SetUtcNow(new DateTimeOffset(2030, 1, 1, 0, 0, 0, TimeSpan.Zero));
        var command = new ReservarTurnoCommand(
            AfiliadoId: Guid.NewGuid(),
            ProfesionalId: Guid.NewGuid(),
            Inicio: new DateTime(2027, 3, 15, 10, 0, 0, DateTimeKind.Utc));

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _handler.HandleAsync(command));
        Assert.Empty(_repository.Turnos);
    }
}

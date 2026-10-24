using Microsoft.Extensions.Time.Testing;
using TurnosMedicos.Application.Tests.Fakes;
using TurnosMedicos.Application.Turnos.CancelarTurno;
using TurnosMedicos.Domain.Entities;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Exceptions;

namespace TurnosMedicos.Application.Tests.Turnos;

public class CancelarTurnoHandlerTests
{
    private static readonly DateTimeOffset Ahora = new(2027, 3, 1, 9, 0, 0, TimeSpan.Zero);

    private readonly InMemoryTurnoRepository _repository = new();
    private readonly FakeTimeProvider _timeProvider = new(Ahora);
    private readonly CancelarTurnoHandler _handler;

    public CancelarTurnoHandlerTests()
    {
        _handler = new CancelarTurnoHandler(_repository, _timeProvider);
    }

    [Fact]
    public async Task HandleAsync_TurnoExistenteConAnticipacionSuficiente_LoCancelaYDevuelveTrue()
    {
        // Arrange
        var turno = AgregarTurno(inicio: Ahora.UtcDateTime.AddDays(14));

        // Act
        var cancelado = await _handler.HandleAsync(new CancelarTurnoCommand(turno.Id));

        // Assert
        Assert.True(cancelado);
        Assert.Equal(EstadoTurno.Cancelado, turno.Estado);
    }

    [Fact]
    public async Task HandleAsync_TurnoInexistente_DevuelveFalse()
    {
        // Act
        var cancelado = await _handler.HandleAsync(new CancelarTurnoCommand(Guid.NewGuid()));

        // Assert
        Assert.False(cancelado);
    }

    [Fact]
    public async Task HandleAsync_ConMenosDe24HorasSegunElReloj_LanzaDomainException()
    {
        // Arrange
        var turno = AgregarTurno(inicio: Ahora.UtcDateTime.AddDays(14));
        _timeProvider.SetUtcNow(new DateTimeOffset(turno.Inicio.AddHours(-2), TimeSpan.Zero));

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(
            () => _handler.HandleAsync(new CancelarTurnoCommand(turno.Id)));
        Assert.Equal(EstadoTurno.Reservado, turno.Estado);
    }

    private Turno AgregarTurno(DateTime inicio)
    {
        var turno = Turno.Reservar(Guid.NewGuid(), Guid.NewGuid(), inicio, Ahora.UtcDateTime);
        _repository.Turnos.Add(turno);
        return turno;
    }
}

using TurnosMedicos.Application.Tests.Fakes;
using TurnosMedicos.Application.Turnos.ObtenerTurno;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Application.Tests.Turnos;

public class ObtenerTurnoHandlerTests
{
    private readonly InMemoryTurnoRepository _repository = new();
    private readonly ObtenerTurnoHandler _handler;

    public ObtenerTurnoHandlerTests()
    {
        _handler = new ObtenerTurnoHandler(_repository);
    }

    [Fact]
    public async Task HandleAsync_TurnoExistente_DevuelveSusDatos()
    {
        // Arrange
        var ahora = new DateTime(2027, 3, 1, 9, 0, 0, DateTimeKind.Utc);
        var turno = Turno.Reservar(Guid.NewGuid(), Guid.NewGuid(), ahora.AddDays(14), ahora);
        _repository.Turnos.Add(turno);

        // Act
        var resultado = await _handler.HandleAsync(new ObtenerTurnoQuery(turno.Id));

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(turno.Id, resultado.Id);
        Assert.Equal(turno.AfiliadoId, resultado.AfiliadoId);
        Assert.Equal(turno.ProfesionalId, resultado.ProfesionalId);
        Assert.Equal(turno.Inicio, resultado.Inicio);
        Assert.Equal("Reservado", resultado.Estado);
    }

    [Fact]
    public async Task HandleAsync_TurnoInexistente_DevuelveNull()
    {
        // Act
        var resultado = await _handler.HandleAsync(new ObtenerTurnoQuery(Guid.NewGuid()));

        // Assert
        Assert.Null(resultado);
    }
}

using TurnosMedicos.Domain.Entities;
using TurnosMedicos.Domain.Enums;

namespace TurnosMedicos.Domain.Tests;

public class TurnoTests
{
    [Fact]
    public void Reservar_ConDatosValidos_QuedaEnEstadoReservado()
    {
        // Arrange
        var afiliadoId = Guid.NewGuid();
        var profesionalId = Guid.NewGuid();
        var inicio = new DateTime(2027, 3, 15, 10, 0, 0);

        // Act
        var turno = Turno.Reservar(afiliadoId, profesionalId, inicio);

        // Assert
        Assert.Equal(EstadoTurno.Reservado, turno.Estado);
    }
}

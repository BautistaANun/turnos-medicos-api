using TurnosMedicos.Domain.Entities;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Exceptions;

namespace TurnosMedicos.Domain.Tests;

public class TurnoTests
{
    private static readonly DateTime Ahora = new(2027, 3, 1, 9, 0, 0);

    [Fact]
    public void Reservar_ConDatosValidos_QuedaEnEstadoReservado()
    {
        // Arrange
        var afiliadoId = Guid.NewGuid();
        var profesionalId = Guid.NewGuid();
        var inicio = new DateTime(2027, 3, 15, 10, 0, 0);

        // Act
        var turno = Turno.Reservar(afiliadoId, profesionalId, inicio, Ahora);

        // Assert
        Assert.Equal(EstadoTurno.Reservado, turno.Estado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-60 * 24)]
    public void Reservar_ConInicioQueNoEsFuturo_LanzaDomainException(int minutosDesdeAhora)
    {
        // Arrange
        var inicio = Ahora.AddMinutes(minutosDesdeAhora);

        // Act & Assert
        Assert.Throws<DomainException>(
            () => Turno.Reservar(Guid.NewGuid(), Guid.NewGuid(), inicio, Ahora));
    }
}

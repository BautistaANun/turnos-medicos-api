using TurnosMedicos.Domain.Entities;
using TurnosMedicos.Domain.Enums;
using TurnosMedicos.Domain.Exceptions;

namespace TurnosMedicos.Domain.Tests;

public class TurnoTests
{
    private static readonly DateTime Ahora = new(2027, 3, 1, 9, 0, 0);
    private static readonly DateTime InicioTurno = new(2027, 3, 15, 10, 0, 0);

    [Fact]
    public void Reservar_ConDatosValidos_QuedaEnEstadoReservado()
    {
        // Arrange
        var afiliadoId = Guid.NewGuid();
        var profesionalId = Guid.NewGuid();

        // Act
        var turno = Turno.Reservar(afiliadoId, profesionalId, InicioTurno, Ahora);

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

    [Theory]
    [InlineData(24)]
    [InlineData(25)]
    [InlineData(72)]
    public void Cancelar_ConAlMenos24HorasDeAnticipacion_QuedaEnEstadoCancelado(int horasDeAnticipacion)
    {
        // Arrange
        var turno = ReservarTurnoValido();
        var momentoCancelacion = turno.Inicio.AddHours(-horasDeAnticipacion);

        // Act
        turno.Cancelar(momentoCancelacion);

        // Assert
        Assert.Equal(EstadoTurno.Cancelado, turno.Estado);
    }

    [Theory]
    [InlineData(23)]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Cancelar_ConMenosDe24HorasDeAnticipacion_LanzaDomainException(int horasDeAnticipacion)
    {
        // Arrange
        var turno = ReservarTurnoValido();
        var momentoCancelacion = turno.Inicio.AddHours(-horasDeAnticipacion);

        // Act & Assert
        Assert.Throws<DomainException>(() => turno.Cancelar(momentoCancelacion));
        Assert.Equal(EstadoTurno.Reservado, turno.Estado);
    }

    [Fact]
    public void Cancelar_TurnoYaCancelado_LanzaDomainException()
    {
        // Arrange
        var turno = ReservarTurnoValido();
        var momentoCancelacion = turno.Inicio.AddDays(-3);
        turno.Cancelar(momentoCancelacion);

        // Act & Assert
        Assert.Throws<DomainException>(() => turno.Cancelar(momentoCancelacion));
    }

    private static Turno ReservarTurnoValido() =>
        Turno.Reservar(Guid.NewGuid(), Guid.NewGuid(), InicioTurno, Ahora);
}

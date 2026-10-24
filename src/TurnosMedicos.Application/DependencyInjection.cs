using Microsoft.Extensions.DependencyInjection;
using TurnosMedicos.Application.Turnos.ReservarTurno;

namespace TurnosMedicos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ReservarTurnoHandler>();

        return services;
    }
}

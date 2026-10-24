using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TurnosMedicos.Infrastructure.Persistence;

public static class MigracionesExtensions
{
    /// <summary>
    /// Aplica las migraciones pendientes. Pensado para desarrollo y demos con Docker Compose;
    /// en producción las migraciones se aplican desde el pipeline de despliegue.
    /// </summary>
    public static async Task AplicarMigracionesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TurnosDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}

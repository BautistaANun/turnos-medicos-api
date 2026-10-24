using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TurnosMedicos.Application.Abstractions;
using TurnosMedicos.Infrastructure.Persistence;
using TurnosMedicos.Infrastructure.Persistence.Repositories;

namespace TurnosMedicos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TurnosDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ITurnoRepository, TurnoRepository>();
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Infrastructure.Persistence;

public class TurnosDbContext(DbContextOptions<TurnosDbContext> options) : DbContext(options)
{
    public DbSet<Turno> Turnos => Set<Turno>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TurnosDbContext).Assembly);
    }
}

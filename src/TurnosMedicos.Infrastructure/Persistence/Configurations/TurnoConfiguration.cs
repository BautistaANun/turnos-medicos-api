using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurnosMedicos.Domain.Entities;

namespace TurnosMedicos.Infrastructure.Persistence.Configurations;

public class TurnoConfiguration : IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.ToTable("Turnos");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        // SQL Server no guarda el "Kind" de un DateTime: al leer lo marcamos como UTC otra vez.
        builder.Property(t => t.Inicio)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(t => t.Estado)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}

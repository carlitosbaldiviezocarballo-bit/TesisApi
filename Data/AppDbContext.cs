using Microsoft.EntityFrameworkCore;
using TesisApi.Domain.Entities;

namespace TesisApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tutor> Tutores => Set<Tutor>();
    public DbSet<Tesis> Tesis => Set<Tesis>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Tutor>(e =>
        {
            e.ToTable("Tutor");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(120);
            e.Property(x => x.Especialidad).IsRequired().HasMaxLength(120);
            e.Property(x => x.Correo).IsRequired().HasMaxLength(150);
            // No puede duplicarse tutor
            e.HasIndex(x => x.Correo).IsUnique();
            e.HasIndex(x => x.Nombre).IsUnique();
        });

        mb.Entity<Tesis>(e =>
        {
            e.ToTable("Tesis");
            e.HasKey(x => x.Id);
            e.Property(x => x.Titulo).IsRequired().HasMaxLength(250);
            e.Property(x => x.Estudiante).IsRequired().HasMaxLength(120);
            e.Property(x => x.Estado).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.Imagen).HasMaxLength(300);
            // No puede duplicarse tesis
            e.HasIndex(x => x.Titulo).IsUnique();
            e.HasOne(x => x.Tutor)
             .WithMany(t => t.Tesis)
             .HasForeignKey(x => x.IdTutor)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

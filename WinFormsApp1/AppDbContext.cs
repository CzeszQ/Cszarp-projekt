using Microsoft.EntityFrameworkCore;
using WinFormsApp1;

public class AppDbContext : DbContext
{
    public DbSet<Pizza> pizze { get; set; }
    public DbSet<Zamowienie> zamowienia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // USUŃ Timezone=UTC z connection string
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pizzeriadb;Username=postgres;Password=haselo123");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja dla timestamp without time zone
        modelBuilder.Entity<Zamowienie>()
            .Property(z => z.datazamowienia)
            .HasColumnType("timestamp without time zone");
    }
}

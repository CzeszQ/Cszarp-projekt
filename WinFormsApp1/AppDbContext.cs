using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WinFormsApp1;
using Npgsql.EntityFrameworkCore.PostgreSQL;

public class AppDbContext : DbContext
{
    public DbSet<Pizza> pizze { get; set; }
    public DbSet<Zamowienie> zamowienia { get; set; } // DODAJ TO

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pizzeriadb;Username=postgres;Password=haselo123");
    }
}

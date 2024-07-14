using SmartCity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace SmartCity.Data
{
    public class SmartCityDbContext : DbContext
    {
        public SmartCityDbContext(DbContextOptions<SmartCityDbContext> options) : base(options)
        {
        }

        public DbSet<Estacionamento> Estacionamentos { get; set; }
        public DbSet<Medicao> Medicoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Estacionamento>(d =>
            { d.Property(e => e.EstacionamentoId).ValueGeneratedOnAdd(); });

            modelBuilder.Entity<Medicao>()
                .HasOne(d => d.Estacionamento)
                .WithMany(m => m.Medicoes)
                .OnDelete(DeleteBehavior.Restrict);


          
        }

     
    }
}

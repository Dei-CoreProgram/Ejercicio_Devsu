using Microsoft.EntityFrameworkCore;
using MicroservicioCliente.Api.Models;

namespace MicroservicioCliente.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            modelBuilder.Entity<Persona>().ToTable("Personas");

            modelBuilder.Entity<Cliente>()
                .HasOne<Persona>() 
                .WithMany() 
                .HasForeignKey(c => c.PersonaId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

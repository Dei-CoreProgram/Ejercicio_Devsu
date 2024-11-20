using Microsoft.EntityFrameworkCore;
using MicroservicioCuenta.Api.Models;

namespace MicroservicioCuenta.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimiento>()
                .Property(m => m.FechaMovimiento)
                .HasColumnType("DATETIME");  

            base.OnModelCreating(modelBuilder);
        }
    }
}

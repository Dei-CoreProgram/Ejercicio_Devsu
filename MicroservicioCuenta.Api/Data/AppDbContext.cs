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

            // Configuración explícita de la relación entre Movimiento y Cuenta
            modelBuilder.Entity<Movimiento>()
                .HasOne<Cuenta>() // Aquí indicamos que Movimiento tiene una relación con Cuenta
                .WithMany() // Indica que Cuenta no tiene propiedad de navegación hacia Movimiento
                .HasForeignKey(m => m.CuentaId) // Indica que CuentaId es la clave foránea
                .OnDelete(DeleteBehavior.Restrict); // Establece el comportamiento de eliminación en cascada (ajústalo según sea necesario)

            base.OnModelCreating(modelBuilder);
        }
    }
}

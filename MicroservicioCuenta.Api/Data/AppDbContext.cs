using Microsoft.EntityFrameworkCore;
using MicroservicioCuenta.Api.Models;
using MicroservicioCliente.Api.Models;

namespace MicroservicioCuenta.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movimiento>()
                .Property(m => m.FechaMovimiento)
                .HasColumnType("DATETIME");

           
            modelBuilder.Entity<Movimiento>()
                .HasOne<Cuenta>() 
                .WithMany() 
                .HasForeignKey(m => m.CuentaId) 
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

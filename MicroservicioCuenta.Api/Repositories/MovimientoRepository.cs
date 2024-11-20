

using MicroservicioCuenta.Api.Data;
using MicroservicioCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MicroservicioCuenta.Api.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly AppDbContext _context;

        public MovimientoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Movimiento> CreateMovimientoAsync(Movimiento movimiento)
        {
            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaId == movimiento.CuentaId);
            if (cuenta == null)
            {
                throw new InvalidOperationException("Cuenta no encontrada.");
            }

            if (movimiento.TipoMovimiento.ToLower() == "retiro" && cuenta.SaldoDisponible < movimiento.Valor)
            {
                throw new InvalidOperationException("Saldo no disponible.");
            }

            cuenta.SaldoDisponible += (movimiento.TipoMovimiento.ToLower() == "deposito" ? movimiento.Valor : -movimiento.Valor);

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return movimiento;
        }
        public async Task<List<Movimiento>> GetMovimientosAsync()
        {
            return await _context.Movimientos.Include(m => m.Cuenta).ToListAsync();
        }


        public async Task<Movimiento> GetMovimientoByIdAsync(int id)
        {
            return await _context.Movimientos.Include(m => m.Cuenta).FirstOrDefaultAsync(m => m.MovimientoId == id);
        }


        public async Task DeleteMovimientoAsync(int id)
        {
            var movimiento = await _context.Movimientos.FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (movimiento != null)
            {
                _context.Movimientos.Remove(movimiento);
                await _context.SaveChangesAsync();
            }
        }


        public async Task UpdateMovimientoAsync(int id, Movimiento movimiento)
        {
            var existingMovimiento = await _context.Movimientos.FirstOrDefaultAsync(m => m.MovimientoId == id);
            if (existingMovimiento == null)
            {
                throw new InvalidOperationException("Movimiento no encontrado.");
            }
            existingMovimiento.TipoMovimiento = movimiento.TipoMovimiento;
            existingMovimiento.Valor = movimiento.Valor;
            existingMovimiento.FechaMovimiento = movimiento.FechaMovimiento;
            if (existingMovimiento.TipoMovimiento.ToLower() == "retiro")
            {
                var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaId == existingMovimiento.CuentaId);
                if (cuenta != null && cuenta.SaldoDisponible < movimiento.Valor)
                {
                    throw new InvalidOperationException("Saldo no disponible para el retiro.");
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}

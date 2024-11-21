using MicroservicioCuenta.Api.Data;
using MicroservicioCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;

public class MovimientoService : IMovimientoService
{
    private readonly AppDbContext _context;

    public MovimientoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Movimiento> CrearMovimientoAsync(Movimiento movimiento)
    {
        _context.Movimientos.Add(movimiento);
        await _context.SaveChangesAsync();
        return movimiento;
    }

    public async Task<List<Movimiento>> GetMovimientosAsync()
    {
        return await _context.Movimientos.ToListAsync();
    }

    public async Task<Movimiento> GetMovimientoByIdAsync(int id)
    {
        return await _context.Movimientos.FindAsync(id);
    }

    public async Task DeleteMovimientoAsync(int id)
    {
        var movimiento = await _context.Movimientos.FindAsync(id);
        if (movimiento != null)
        {
            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Cuenta> GetCuentaByIdAsync(int cuentaId)
    {
        return await _context.Cuentas.FindAsync(cuentaId);
    }

    public async Task ActualizarCuentaAsync(Cuenta cuenta)
    {
        _context.Cuentas.Update(cuenta);
        await _context.SaveChangesAsync();
    }

    public List<Movimiento> ObtenerMovimientosPorCuentaYFecha(int cuentaId, DateTime fechaInicio, DateTime fechaFin)
    {
        return _context.Movimientos
            .Where(m => m.CuentaId == cuentaId && m.FechaMovimiento >= fechaInicio && m.FechaMovimiento <= fechaFin)
            .ToList();
    }

}

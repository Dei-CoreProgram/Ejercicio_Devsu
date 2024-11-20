using MicroservicioCuenta.Api.Data;
using MicroservicioCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroservicioCuenta.Api.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly AppDbContext _context;
        public CuentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cuenta> GetCuentaByIdAsync(int id)
        {
            return await _context.Cuentas.FindAsync(id); 
        }

        public async Task<List<Cuenta>> GetCuentasAsync()
        {
            return await _context.Cuentas.ToListAsync(); 
        }
        public async Task<Cuenta> CreateCuentaAsync(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync(); 
            return cuenta; 
        }

        public async Task<Cuenta> UpdateCuentaAsync(int id, Cuenta cuenta)
        {
            var existingCuenta = await _context.Cuentas.FindAsync(id);
            if (existingCuenta == null)
            {
                return null; 
            }
            existingCuenta.NumeroCuenta = cuenta.NumeroCuenta;
            existingCuenta.TipoCuenta = cuenta.TipoCuenta;
            existingCuenta.SaldoInicial = cuenta.SaldoInicial;
            existingCuenta.SaldoDisponible = cuenta.SaldoDisponible;
            existingCuenta.Estado = cuenta.Estado;

            await _context.SaveChangesAsync();

            return existingCuenta;
        }

        public async Task DeleteCuentaAsync(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta != null)
            {
                _context.Cuentas.Remove(cuenta); 
                await _context.SaveChangesAsync(); 
            }
        }
    }
}

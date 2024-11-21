

using MicroservicioCuenta.Api.Data;
using MicroservicioCuenta.Api.Models;
using MicroservicioCuenta.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioCuenta.Api.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly AppDbContext _context;

        public CuentaService(ICuentaRepository cuentaRepository, AppDbContext context)
        {
            _cuentaRepository = cuentaRepository;
            _context = context;
        }
        public async Task<Cuenta> GetCuentaByIdAsync(int id)
        {
            return await _cuentaRepository.GetCuentaByIdAsync(id);
        }

        public async Task<List<Cuenta>> GetAllCuentasAsync()
        {
            return await _cuentaRepository.GetCuentasAsync();
        }

        public async Task<Cuenta> CreateCuentaAsync(Cuenta cuenta)
        {
            return await _cuentaRepository.CreateCuentaAsync(cuenta);
        }

        public async Task UpdateCuentaAsync(int id, Cuenta cuenta)
        {
            await _cuentaRepository.UpdateCuentaAsync(id, cuenta);
        }

        public async Task DeleteCuentaAsync(int id)
        {
            await _cuentaRepository.DeleteCuentaAsync(id);
        }

        public List<Cuenta> ObtenerCuentasPorCliente(int clienteId)
        {
            return _context.Cuentas.Where(c => c.ClienteId == clienteId).ToList();
        }
    }
}


using MicroservicioCuenta.Api.Models;

namespace MicroservicioCuenta.Api.Services
{
    public interface ICuentaService
    {
        Task<Cuenta> GetCuentaByIdAsync(int id);
        Task<List<Cuenta>> GetAllCuentasAsync();
        Task<Cuenta> CreateCuentaAsync(Cuenta cuenta);
        Task UpdateCuentaAsync(int id, Cuenta cuenta);
        Task DeleteCuentaAsync(int id);
    }
}

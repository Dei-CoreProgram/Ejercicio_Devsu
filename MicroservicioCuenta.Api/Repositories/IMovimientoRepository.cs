
using MicroservicioCuenta.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioCuenta.Api.Repositories
{
    public interface IMovimientoRepository
    {
        Task<Movimiento> CreateMovimientoAsync(Movimiento movimiento);
        Task<List<Movimiento>> GetMovimientosAsync();
        Task<Movimiento> GetMovimientoByIdAsync(int id);
        Task DeleteMovimientoAsync(int id);
        Task UpdateMovimientoAsync(int id, Movimiento movimiento);  
    }
}

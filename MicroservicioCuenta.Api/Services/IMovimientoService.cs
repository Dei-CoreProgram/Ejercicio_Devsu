using MicroservicioCuenta.Api.Models;

public interface IMovimientoService
{
    Task<Movimiento> CrearMovimientoAsync(Movimiento movimiento);
    Task<List<Movimiento>> GetMovimientosAsync();
    Task<Movimiento> GetMovimientoByIdAsync(int id);
    Task DeleteMovimientoAsync(int id);
    Task<Cuenta> GetCuentaByIdAsync(int cuentaId);  
    Task ActualizarCuentaAsync(Cuenta cuenta);
}

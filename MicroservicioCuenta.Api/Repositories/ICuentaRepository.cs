using MicroservicioCuenta.Api.Models;

public interface ICuentaRepository
{
    Task<Cuenta> GetCuentaByIdAsync(int id);
    Task<List<Cuenta>> GetCuentasAsync();  
    Task<Cuenta> CreateCuentaAsync(Cuenta cuenta);
    Task<Cuenta> UpdateCuentaAsync(int id, Cuenta cuenta);
    Task DeleteCuentaAsync(int id);
}

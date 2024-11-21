using MicroservicioCuenta.Api.Models;
using MicroservicioCuenta.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentasController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            var cuentas = await _cuentaService.GetAllCuentasAsync();
            return Ok(cuentas); 
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuentaById(int id)
        {
            var cuenta = await _cuentaService.GetCuentaByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound(); 
            }

            return Ok(cuenta); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateCuenta([FromBody] Cuenta cuenta)
        {
            if (cuenta == null)
            {
                return BadRequest("La cuenta no puede ser nula."); 
            }

            var nuevaCuenta = await _cuentaService.CreateCuentaAsync(cuenta); 
            return CreatedAtAction(nameof(GetCuentaById), new { id = nuevaCuenta.CuentaId }, nuevaCuenta);
        }
        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, [FromBody] Cuenta cuenta)
        {
            if (cuenta == null || id != cuenta.CuentaId)
            {
                return BadRequest("Datos inválidos o el id no coincide.");
            }

            await _cuentaService.UpdateCuentaAsync(id, cuenta); 
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _cuentaService.GetCuentaByIdAsync(id);
            if (cuenta == null)
            {
                return NotFound(); 
            }

            await _cuentaService.DeleteCuentaAsync(id); 
            return NoContent(); 
        }
    }
}

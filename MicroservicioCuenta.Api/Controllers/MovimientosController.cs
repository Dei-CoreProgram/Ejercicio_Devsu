using MicroservicioCuenta.Api.Models;
using MicroservicioCuenta.Api.Services; // Importa el servicio
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroservicioCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService; 

        public MovimientosController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        // post
        [HttpPost]
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoDto movimientoDto)
        {
            if (movimientoDto == null)
            {
                return BadRequest("Los datos del movimiento son inválidos.");
            }

   
            var cuenta = await _movimientoService.GetCuentaByIdAsync(movimientoDto.CuentaId);
            if (cuenta == null)
            {
                return BadRequest("La cuenta especificada no existe.");
            }

            if (movimientoDto.TipoMovimiento == "Retiro" && cuenta.SaldoDisponible < movimientoDto.Valor)
            {
                return BadRequest("El saldo disponible es insuficiente para realizar este retiro.");
            }

            var movimiento = new Movimiento
            {
                FechaMovimiento = movimientoDto.FechaMovimiento,
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Valor = movimientoDto.Valor,
                Saldo = movimientoDto.TipoMovimiento == "Depósito"
                    ? cuenta.SaldoDisponible + movimientoDto.Valor  
                    : cuenta.SaldoDisponible - movimientoDto.Valor,
                CuentaId = movimientoDto.CuentaId
            };

            try
            {
                await _movimientoService.CrearMovimientoAsync(movimiento);

                cuenta.SaldoDisponible = movimiento.Saldo;  

                await _movimientoService.ActualizarCuentaAsync(cuenta);

                return CreatedAtAction(nameof(GetMovimientoById), new { id = movimiento.MovimientoId }, movimiento);
            }
            catch (Exception ex)
            {
  
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        // get
        [HttpGet]
        public async Task<IActionResult> GetMovimientos()
        {
            var movimientos = await _movimientoService.GetMovimientosAsync();
            return Ok(movimientos);
        }

        //get x id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _movimientoService.GetMovimientoByIdAsync(id);

            if (movimiento == null)
            {
                return NotFound($"No se encontró el movimiento con ID {id}.");
            }

            return Ok(movimiento);
        }

        // delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _movimientoService.GetMovimientoByIdAsync(id);

            if (movimiento == null)
            {
                return NotFound($"No se encontró el movimiento con ID {id}.");
            }

            await _movimientoService.DeleteMovimientoAsync(id);
            return NoContent();
        }
    }
}

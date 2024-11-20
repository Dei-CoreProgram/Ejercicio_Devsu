using MicroservicioCuenta.Api.Models;
using MicroservicioCuenta.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;

        public MovimientosController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> CreateMovimiento([FromBody] Movimiento movimiento)
        {
            if (movimiento == null)
            {
                return BadRequest("Movimiento inválido.");
            }

            try
            {
                var createdMovimiento = await _movimientoRepository.CreateMovimientoAsync(movimiento);
                return CreatedAtAction(nameof(GetMovimientoById), new { id = createdMovimiento.MovimientoId }, createdMovimiento);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetMovimientos()
        {
            var movimientos = await _movimientoRepository.GetMovimientosAsync();
            return Ok(movimientos);
        }

        //GET X ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdAsync(id);

            if (movimiento == null)
            {
                return NotFound($"No se encontró el movimiento con ID {id}.");
            }

            return Ok(movimiento);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _movimientoRepository.GetMovimientoByIdAsync(id);

            if (movimiento == null)
            {
                return NotFound($"No se encontró el movimiento con ID {id}.");
            }

            await _movimientoRepository.DeleteMovimientoAsync(id);
            return NoContent(); 
        }

  //PUT

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovimiento(int id, [FromBody] Movimiento movimiento)
        {
            try
            {
                await _movimientoRepository.UpdateMovimientoAsync(id, movimiento);
                return Ok("Movimiento actualizado exitosamente.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}

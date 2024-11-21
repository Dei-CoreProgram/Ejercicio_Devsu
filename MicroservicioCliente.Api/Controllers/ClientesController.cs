using Microsoft.AspNetCore.Mvc;
using MicroservicioCliente.Api.Models;
using MicroservicioCliente.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroservicioCliente.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteRepository.GetAllClientesAsync();
            return Ok(clientes);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Cliente data is required.");
            }

            // Crear el Cliente (la Persona se guarda automáticamente como parte de Cliente)
            await _clienteRepository.AddClienteAsync(cliente);

            // Usamos 'Id' en vez de 'ClienteId' ya que ahora es la clave primaria heredada de Persona
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id) // Usamos 'Id' en lugar de 'ClienteId'
            {
                return BadRequest();
            }

            await _clienteRepository.UpdateClienteAsync(cliente);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _clienteRepository.DeleteClienteAsync(id);
            return NoContent();
        }
    }
}

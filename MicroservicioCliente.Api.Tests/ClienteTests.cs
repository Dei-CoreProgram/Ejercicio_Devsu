using MicroservicioCliente.Api.Data;
using MicroservicioCliente.Api.Models;
using MicroservicioCliente.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroservicioCliente.Api.Tests
{
    public class ClienteTests
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly DbContextOptions<AppDbContext> _options;

        public ClienteTests()
        {
            // Configurar la base de datos en memoria
            _options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb")
                        .Options;

            // Crear una instancia del repositorio con la base de datos en memoria
            var context = new AppDbContext(_options);
            _clienteRepository = new ClienteRepository(context);
        }

        [Fact]
        public async Task CrearCliente_ClienteValido_RetornaCliente()
        {
            // Arrange: Crear un cliente nuevo
            var nuevoCliente = new Cliente
            {
                Nombre = "Juan Pérez",
                Genero = "Masculino",
                Edad = 30,
                Identificacion = "1234567890",
                Direccion = "Calle Falsa 123",
                Telefono = "9876543210",
                Contrasena = "password123",
                Estado = "Activo"
            };

            // Act: Agregar el cliente a la base de datos
            await _clienteRepository.AddClienteAsync(nuevoCliente);

            // Assert: Verificar si el cliente fue guardado
            using (var context = new AppDbContext(_options))
            {
                var clienteGuardado = await context.Clientes
                                                    .Where(c => c.Nombre == "Juan Pérez")
                                                    .FirstOrDefaultAsync();

                Assert.NotNull(clienteGuardado);
                Assert.Equal("Juan Pérez", clienteGuardado.Nombre);
                Assert.Equal(30, clienteGuardado.Edad);
                Assert.Equal("Activo", clienteGuardado.Estado);
            }
        }
    }
}

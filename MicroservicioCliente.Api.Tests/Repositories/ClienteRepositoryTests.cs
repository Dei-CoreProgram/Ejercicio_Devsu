using MicroservicioCliente.Api.Data;
using MicroservicioCliente.Api.Models;
using MicroservicioCliente.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MicroservicioCliente.Api.Tests.Repositories
{
    public class ClienteRepositoryTests
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly IClienteRepository _clienteRepository;

        public ClienteRepositoryTests()
        {
            // Configurar la base de datos en memoria
            _options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: "TestDb")
                        .Options;

            var context = new AppDbContext(_options);
            _clienteRepository = new ClienteRepository(context);
        }

        [Fact]
        public async Task AddClienteAsync_CrearCliente_RetornaCliente()
        {
            // Arrange: Crear un cliente nuevo
            var nuevoCliente = new Cliente
            {
                Nombre = "Ana Gómez",
                Genero = "Femenino",
                Edad = 28,
                Identificacion = "9876543210",
                Direccion = "Av. Siempre Viva 456",
                Telefono = "1234567890",
                Contrasena = "contraseña456",
                Estado = "Activo"
            };

            // Act: Agregar el cliente al repositorio
            await _clienteRepository.AddClienteAsync(nuevoCliente);

            // Assert: Verificar que el cliente fue agregado correctamente
            using (var context = new AppDbContext(_options))
            {
                var clienteGuardado = await context.Clientes
                                                    .Where(c => c.Nombre == "Ana Gómez")
                                                    .FirstOrDefaultAsync();

                Assert.NotNull(clienteGuardado);
                Assert.Equal("Ana Gómez", clienteGuardado.Nombre);
            }
        }

        [Fact]
        public async Task GetClienteByIdAsync_ClienteExistente_RetornaCliente()
        {
            // Arrange: Crear y agregar un cliente
            var cliente = new Cliente
            {
                Nombre = "Carlos López",
                Genero = "Masculino",
                Edad = 35,
                Identificacion = "1122334455",
                Direccion = "Calle 10 789",
                Telefono = "5566778899",
                Contrasena = "password789",
                Estado = "Activo"
            };

            using (var context = new AppDbContext(_options))
            {
                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
            }

            // Act: Obtener el cliente por ID
            Cliente clienteEncontrado;
            using (var context = new AppDbContext(_options))
            {
                var repositorio = new ClienteRepository(context);
                clienteEncontrado = await repositorio.GetClienteByIdAsync(cliente.Id);
            }

            // Assert: Verificar que el cliente fue encontrado
            Assert.NotNull(clienteEncontrado);
            Assert.Equal("Carlos López", clienteEncontrado.Nombre);
            Assert.Equal(35, clienteEncontrado.Edad);
        }
    }
}

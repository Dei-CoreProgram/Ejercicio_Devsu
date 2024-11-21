using MicroservicioCliente.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using MicroservicioCliente.Api.Models;

namespace MicroservicioCliente.Api.Tests.Integración
{
    public class ClienteIntegraciónTests : IClassFixture<WebApplicationFactory<Program>>  // Usa 'Program' en lugar de 'MicroservicioCliente.Api.Program'
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ClienteIntegraciónTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task CrearCliente_CrearCliente_RetornaClienteCreado()
        {
            var nuevoCliente = new Cliente
            {
                Nombre = "Pedro Gómez",
                Genero = "Masculino",
                Edad = 40,
                Identificacion = "1122334455",
                Direccion = "Calle Nueva 101",
                Telefono = "4567891230",
                Contrasena = "contraseña789",
                Estado = "Activo"
            };

            var clienteJson = JsonConvert.SerializeObject(nuevoCliente);
            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/clientes", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            var clienteCreadoJson = await response.Content.ReadAsStringAsync();
            var clienteCreado = JsonConvert.DeserializeObject<Cliente>(clienteCreadoJson);

            Assert.NotNull(clienteCreado);
            Assert.Equal("Pedro Gómez", clienteCreado.Nombre);
            Assert.Equal(40, clienteCreado.Edad);
            Assert.Equal("Activo", clienteCreado.Estado);
        }
    }
}

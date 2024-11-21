using MicroservicioCliente.Api.Models;

namespace MicroservicioCuenta.Api.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string Estado { get; set; }

        // Relación muchos a uno: muchas cuentas pertenecen a un cliente
        public int ClienteId { get; set; }  // Clave foránea a Cliente

    }
}

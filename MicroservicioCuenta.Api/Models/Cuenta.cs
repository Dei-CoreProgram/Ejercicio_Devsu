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
    }
}

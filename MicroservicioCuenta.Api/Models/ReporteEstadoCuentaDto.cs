namespace MicroservicioCuenta.Api.Models
{
    public class ReporteEstadoCuentaDto
    {
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoDisponible { get; set; }
        public List<MovimientoDto> Movimientos { get; set; }  // Detalle de movimientos
    }
}

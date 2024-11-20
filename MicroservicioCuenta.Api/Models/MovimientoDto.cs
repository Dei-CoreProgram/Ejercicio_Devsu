namespace MicroservicioCuenta.Api.Models
{
    public class MovimientoDto
    {
        public DateTime FechaMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int CuentaId { get; set; } 
    }
}

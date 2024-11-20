using System;

namespace MicroservicioCuenta.Api.Models
{
    public class Movimiento
    {
        public int MovimientoId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
    }
}

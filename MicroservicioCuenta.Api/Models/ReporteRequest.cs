namespace MicroservicioCuenta.Api.Models
{
    public class ReporteRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int ClienteId { get; set; }
    }
}

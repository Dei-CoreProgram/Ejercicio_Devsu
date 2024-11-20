﻿namespace MicroservicioCuenta.Api.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; }
    }
}

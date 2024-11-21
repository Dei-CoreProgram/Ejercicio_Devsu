using Microsoft.AspNetCore.Mvc;
using MicroservicioCuenta.Api.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using MicroservicioCuenta.Api.Services;

namespace MicroservicioCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ICuentaService _cuentaService; 
        private readonly IMovimientoService _movimientoService; 

        public ReportesController(ICuentaService cuentaService, IMovimientoService movimientoService)
        {
            _cuentaService = cuentaService;
            _movimientoService = movimientoService;
        }

        [HttpGet("reportes")]
        public ActionResult<List<ReporteEstadoCuentaDto>> GenerarReporteEstadoCuenta(DateTime fechaInicio, DateTime fechaFin, int clienteId)
        {
            var cuentas = _cuentaService.ObtenerCuentasPorCliente(clienteId);
            if (cuentas == null || cuentas.Count == 0)
            {
                return NotFound("No se encontraron cuentas para este cliente.");
            }

            var reporte = new List<ReporteEstadoCuentaDto>();

            foreach (var cuenta in cuentas)
            {

                var movimientos = _movimientoService.ObtenerMovimientosPorCuentaYFecha(cuenta.CuentaId, fechaInicio, fechaFin);

 
                var reporteCuenta = new ReporteEstadoCuentaDto
                {
                    NumeroCuenta = cuenta.NumeroCuenta,
                    TipoCuenta = cuenta.TipoCuenta,
                    SaldoInicial = cuenta.SaldoInicial,
                    SaldoDisponible = cuenta.SaldoDisponible,
                    Movimientos = movimientos.Select(mov => new MovimientoDto
                    {
                        FechaMovimiento = mov.FechaMovimiento,
                        TipoMovimiento = mov.TipoMovimiento,
                        Valor = mov.Valor,
                        Saldo = mov.Saldo,
                        CuentaId = mov.CuentaId
                    }).ToList()
                };

                reporte.Add(reporteCuenta);
            }
            return Ok(reporte);
        }
    }
}

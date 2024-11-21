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
        private readonly ICuentaService _cuentaService;  // Dependencia para obtener las cuentas
        private readonly IMovimientoService _movimientoService;  // Dependencia para obtener los movimientos

        public ReportesController(ICuentaService cuentaService, IMovimientoService movimientoService)
        {
            _cuentaService = cuentaService;
            _movimientoService = movimientoService;
        }

        [HttpGet("reportes")]
        public ActionResult<List<ReporteEstadoCuentaDto>> GenerarReporteEstadoCuenta(DateTime fechaInicio, DateTime fechaFin, int clienteId)
        {
            // 1. Obtener todas las cuentas del cliente
            var cuentas = _cuentaService.ObtenerCuentasPorCliente(clienteId);
            if (cuentas == null || cuentas.Count == 0)
            {
                return NotFound("No se encontraron cuentas para este cliente.");
            }

            var reporte = new List<ReporteEstadoCuentaDto>();

            foreach (var cuenta in cuentas)
            {
                // 2. Obtener los movimientos dentro del rango de fechas para cada cuenta
                var movimientos = _movimientoService.ObtenerMovimientosPorCuentaYFecha(cuenta.CuentaId, fechaInicio, fechaFin);

                // 3. Crear el DTO para el reporte con la información de la cuenta y los movimientos
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

            // 4. Retornar el reporte en formato JSON
            return Ok(reporte);
        }
    }
}

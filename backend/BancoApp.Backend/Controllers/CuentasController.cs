using Microsoft.AspNetCore.Mvc;
using BancoApp.Backend.Services;
using BancoApp.Backend.Models;

namespace BancoApp.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentasController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cuenta>>> Listar()
    {
        var cuentas = await _cuentaService.ListarCuentasAsync();
        return Ok(cuentas);
    }

    [HttpGet("saldos")]
    public async Task<ActionResult<List<Cuenta>>> ConsultarSaldos()
    {
        var cuentas = await _cuentaService.ConsultarSaldosAsync();
        return Ok(cuentas);
    }

    [HttpGet("{nroCuenta}")]
    public async Task<ActionResult<Cuenta>> Obtener(string nroCuenta)
    {
        var cuenta = await _cuentaService.ObtenerCuentaAsync(nroCuenta);
        if (cuenta == null)
            return NotFound(new { Mensaje = "Cuenta no encontrada." });
        return Ok(cuenta);
    }

    [HttpPost]
    public async Task<ActionResult<ResultadoOperacion>> Crear([FromBody] CrearCuentaRequest request)
    {
        var resultado = await _cuentaService.CrearCuentaAsync(request);
        if (!resultado.Exito)
            return BadRequest(resultado);
        return Ok(resultado);
    }

    [HttpGet("monedas")]
    public async Task<ActionResult<List<Moneda>>> ListarMonedas()
    {
        var monedas = await _cuentaService.ListarMonedasAsync();
        return Ok(monedas);
    }
}

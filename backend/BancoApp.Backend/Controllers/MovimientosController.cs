using Microsoft.AspNetCore.Mvc;
using BancoApp.Backend.Services;
using BancoApp.Backend.Models;

namespace BancoApp.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoService _movimientoService;

    public MovimientosController(IMovimientoService movimientoService)
    {
        _movimientoService = movimientoService;
    }

    [HttpPost("abono")]
    public async Task<ActionResult<ResultadoOperacion>> RegistrarAbono([FromBody] MovimientoRequest request)
    {
        var resultado = await _movimientoService.RegistrarAbonoAsync(request);
        if (!resultado.Exito)
            return BadRequest(resultado);
        return Ok(resultado);
    }

    [HttpPost("retiro")]
    public async Task<ActionResult<ResultadoOperacion>> RegistrarRetiro([FromBody] MovimientoRequest request)
    {
        var resultado = await _movimientoService.RegistrarRetiroAsync(request);
        if (!resultado.Exito)
            return BadRequest(resultado);
        return Ok(resultado);
    }

    [HttpPost("transferencia")]
    public async Task<ActionResult<ResultadoOperacion>> RealizarTransferencia([FromBody] TransferenciaRequest request)
    {
        var resultado = await _movimientoService.RealizarTransferenciaAsync(request);
        if (!resultado.Exito)
            return BadRequest(resultado);
        return Ok(resultado);
    }

    [HttpGet("{nroCuenta}")]
    public async Task<ActionResult<List<Movimiento>>> ConsultarMovimientos(string nroCuenta)
    {
        var movimientos = await _movimientoService.ConsultarMovimientosAsync(nroCuenta);
        return Ok(movimientos);
    }
}

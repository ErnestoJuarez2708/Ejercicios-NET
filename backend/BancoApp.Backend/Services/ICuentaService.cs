using BancoApp.Backend.Models;

namespace BancoApp.Backend.Services;

public interface ICuentaService
{
    Task<List<Cuenta>> ListarCuentasAsync();
    Task<List<Cuenta>> ConsultarSaldosAsync();
    Task<Cuenta?> ObtenerCuentaAsync(string nroCuenta);
    Task<ResultadoOperacion> CrearCuentaAsync(CrearCuentaRequest request);
    Task<List<Moneda>> ListarMonedasAsync();
}

public interface IMovimientoService
{
    Task<ResultadoOperacion> RegistrarAbonoAsync(MovimientoRequest request);
    Task<ResultadoOperacion> RegistrarRetiroAsync(MovimientoRequest request);
    Task<ResultadoOperacion> RealizarTransferenciaAsync(TransferenciaRequest request);
    Task<List<Movimiento>> ConsultarMovimientosAsync(string nroCuenta);
}

using BancoApp.Backend.Data;
using BancoApp.Backend.Models;

namespace BancoApp.Backend.Services;

public class MovimientoService : IMovimientoService
{
    private readonly DatabaseHelper _db;

    public MovimientoService(DatabaseHelper db)
    {
        _db = db;
    }

    public async Task<ResultadoOperacion> RegistrarAbonoAsync(MovimientoRequest request)
    {
        if (request.Importe <= 0)
            return new ResultadoOperacion { Exito = false, Mensaje = "El importe debe ser mayor a cero." };

        if (string.IsNullOrWhiteSpace(request.NroCuenta))
            return new ResultadoOperacion { Exito = false, Mensaje = "El numero de cuenta es requerido." };

        if (string.IsNullOrWhiteSpace(request.Glosa))
            return new ResultadoOperacion { Exito = false, Mensaje = "La glosa es requerida." };

        return await _db.RegistrarAbonoAsync(request.NroCuenta, request.Importe, request.Glosa);
    }

    public async Task<ResultadoOperacion> RegistrarRetiroAsync(MovimientoRequest request)
    {
        if (request.Importe <= 0)
            return new ResultadoOperacion { Exito = false, Mensaje = "El importe debe ser mayor a cero." };

        if (string.IsNullOrWhiteSpace(request.NroCuenta))
            return new ResultadoOperacion { Exito = false, Mensaje = "El numero de cuenta es requerido." };

        if (string.IsNullOrWhiteSpace(request.Glosa))
            return new ResultadoOperacion { Exito = false, Mensaje = "La glosa es requerida." };

        // Verificar saldo suficiente
        var cuenta = await _db.ObtenerCuentaAsync(request.NroCuenta);
        if (cuenta == null)
            return new ResultadoOperacion { Exito = false, Mensaje = "La cuenta no existe." };

        if (cuenta.Saldo < request.Importe)
            return new ResultadoOperacion { Exito = false, Mensaje = $"Saldo insuficiente. Saldo actual: {cuenta.Saldo:F2}" };

        return await _db.RegistrarRetiroAsync(request.NroCuenta, request.Importe, request.Glosa);
    }

    public async Task<ResultadoOperacion> RealizarTransferenciaAsync(TransferenciaRequest request)
    {
        if (request.Importe <= 0)
            return new ResultadoOperacion { Exito = false, Mensaje = "El importe debe ser mayor a cero." };

        if (request.CuentaOrigen == request.CuentaDestino)
            return new ResultadoOperacion { Exito = false, Mensaje = "No se puede transferir a la misma cuenta." };

        if (string.IsNullOrWhiteSpace(request.CuentaOrigen) || string.IsNullOrWhiteSpace(request.CuentaDestino))
            return new ResultadoOperacion { Exito = false, Mensaje = "Las cuentas origen y destino son requeridas." };

        if (string.IsNullOrWhiteSpace(request.Glosa))
            return new ResultadoOperacion { Exito = false, Mensaje = "La glosa es requerida." };

        // Verificar saldo suficiente en origen
        var cuentaOrigen = await _db.ObtenerCuentaAsync(request.CuentaOrigen);
        if (cuentaOrigen == null)
            return new ResultadoOperacion { Exito = false, Mensaje = "La cuenta origen no existe." };

        if (cuentaOrigen.Saldo < request.Importe)
            return new ResultadoOperacion { Exito = false, Mensaje = $"Saldo insuficiente en cuenta origen. Saldo actual: {cuentaOrigen.Saldo:F2}" };

        // Verificar que cuenta destino exista
        var cuentaDestino = await _db.ObtenerCuentaAsync(request.CuentaDestino);
        if (cuentaDestino == null)
            return new ResultadoOperacion { Exito = false, Mensaje = "La cuenta destino no existe." };

        return await _db.RealizarTransferenciaAsync(request.CuentaOrigen, request.CuentaDestino, request.Importe, request.Glosa);
    }

    public async Task<List<Movimiento>> ConsultarMovimientosAsync(string nroCuenta)
    {
        return await _db.ConsultarMovimientosAsync(nroCuenta);
    }
}

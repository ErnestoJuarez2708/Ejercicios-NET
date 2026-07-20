using BancoApp.Backend.Data;
using BancoApp.Backend.Models;

namespace BancoApp.Backend.Services;

public class CuentaService : ICuentaService
{
    private readonly DatabaseHelper _db;

    public CuentaService(DatabaseHelper db)
    {
        _db = db;
    }

    public async Task<List<Cuenta>> ListarCuentasAsync()
    {
        return await _db.ListarCuentasAsync();
    }

    public async Task<List<Cuenta>> ConsultarSaldosAsync()
    {
        return await _db.ConsultarSaldosAsync();
    }

    public async Task<Cuenta?> ObtenerCuentaAsync(string nroCuenta)
    {
        return await _db.ObtenerCuentaAsync(nroCuenta);
    }

    public async Task<ResultadoOperacion> CrearCuentaAsync(CrearCuentaRequest request)
    {
        // Validaciones de negocio
        if (string.IsNullOrWhiteSpace(request.NroCuenta))
            return new ResultadoOperacion { Exito = false, Mensaje = "El numero de cuenta es requerido." };

        if (request.Tipo != "AHO" && request.Tipo != "CTE")
            return new ResultadoOperacion { Exito = false, Mensaje = "Tipo de cuenta invalido. Use AHO o CTE." };

        if (request.Tipo == "CTE" && request.NroCuenta.Length != 13)
            return new ResultadoOperacion { Exito = false, Mensaje = "La cuenta corriente debe tener 13 caracteres." };

        if (request.Tipo == "AHO" && request.NroCuenta.Length != 14)
            return new ResultadoOperacion { Exito = false, Mensaje = "La cuenta de ahorro debe tener 14 caracteres." };

        if (string.IsNullOrWhiteSpace(request.Nombre))
            return new ResultadoOperacion { Exito = false, Mensaje = "El nombre del titular es requerido." };

        var cuenta = new Cuenta
        {
            NroCuenta = request.NroCuenta,
            Tipo = request.Tipo,
            Moneda = request.Moneda,
            Nombre = request.Nombre
        };

        return await _db.CrearCuentaAsync(cuenta);
    }

    public async Task<List<Moneda>> ListarMonedasAsync()
    {
        return await _db.ListarMonedasAsync();
    }
}

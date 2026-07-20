using Microsoft.Data.SqlClient;
using BancoApp.Backend.Models;

namespace BancoApp.Backend.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("BancoDB")
            ?? throw new InvalidOperationException("Connection string 'BancoDB' not found.");
    }

    private SqlConnection GetConnection() => new(_connectionString);

    // ==================== CUENTAS ====================

    public async Task<List<Cuenta>> ListarCuentasAsync()
    {
        var cuentas = new List<Cuenta>();
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_LISTAR_CUENTAS", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            cuentas.Add(new Cuenta
            {
                NroCuenta = reader["NRO_CUENTA"].ToString()!,
                Tipo = reader["TIPO"].ToString()!,
                Moneda = reader["MONEDA"].ToString()!,
                Nombre = reader["NOMBRE"].ToString()!,
                Saldo = Convert.ToDecimal(reader["SALDO"]),
                MonedaNombre = reader["MONEDA_NOMBRE"].ToString()!
            });
        }
        return cuentas;
    }

    public async Task<List<Cuenta>> ConsultarSaldosAsync()
    {
        var cuentas = new List<Cuenta>();
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_CONSULTAR_SALDOS", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            cuentas.Add(new Cuenta
            {
                Tipo = reader["TIPO"].ToString()!,
                Moneda = reader["MONEDA"].ToString()!,
                NroCuenta = reader["NRO_CUENTA"].ToString()!,
                Nombre = reader["TITULAR"].ToString()!,
                Saldo = Convert.ToDecimal(reader["SALDO"])
            });
        }
        return cuentas;
    }

    public async Task<Cuenta?> ObtenerCuentaAsync(string nroCuenta)
    {
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_OBTENER_CUENTA", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@NRO_CUENTA", nroCuenta);
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Cuenta
            {
                NroCuenta = reader["NRO_CUENTA"].ToString()!,
                Tipo = reader["TIPO"].ToString()!,
                Moneda = reader["MONEDA"].ToString()!,
                Nombre = reader["NOMBRE"].ToString()!,
                Saldo = Convert.ToDecimal(reader["SALDO"]),
                MonedaNombre = reader["MONEDA_NOMBRE"].ToString()!
            };
        }
        return null;
    }

    public async Task<ResultadoOperacion> CrearCuentaAsync(Cuenta cuenta)
    {
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_CREAR_CUENTA", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@NRO_CUENTA", cuenta.NroCuenta);
        command.Parameters.AddWithValue("@TIPO", cuenta.Tipo);
        command.Parameters.AddWithValue("@MONEDA", cuenta.Moneda);
        command.Parameters.AddWithValue("@NOMBRE", cuenta.Nombre);

        try
        {
            await command.ExecuteNonQueryAsync();
            return new ResultadoOperacion { Exito = true, Mensaje = "Cuenta creada exitosamente." };
        }
        catch (SqlException ex)
        {
            return new ResultadoOperacion { Exito = false, Mensaje = ex.Message };
        }
    }

    // ==================== MOVIMIENTOS ====================

    public async Task<ResultadoOperacion> RegistrarAbonoAsync(string nroCuenta, decimal importe, string glosa)
    {
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_REGISTRAR_ABONO", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@NRO_CUENTA", nroCuenta);
        command.Parameters.AddWithValue("@IMPORTE", importe);
        command.Parameters.AddWithValue("@GLOSA", glosa);

        try
        {
            await command.ExecuteNonQueryAsync();
            return new ResultadoOperacion { Exito = true, Mensaje = "Deposito registrado exitosamente." };
        }
        catch (SqlException ex)
        {
            return new ResultadoOperacion { Exito = false, Mensaje = ex.Message };
        }
    }

    public async Task<ResultadoOperacion> RegistrarRetiroAsync(string nroCuenta, decimal importe, string glosa)
    {
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_REGISTRAR_RETIRO", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@NRO_CUENTA", nroCuenta);
        command.Parameters.AddWithValue("@IMPORTE", importe);
        command.Parameters.AddWithValue("@GLOSA", glosa);

        try
        {
            await command.ExecuteNonQueryAsync();
            return new ResultadoOperacion { Exito = true, Mensaje = "Retiro registrado exitosamente." };
        }
        catch (SqlException ex)
        {
            return new ResultadoOperacion { Exito = false, Mensaje = ex.Message };
        }
    }

    public async Task<ResultadoOperacion> RealizarTransferenciaAsync(string cuentaOrigen, string cuentaDestino, decimal importe, string glosa)
    {
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_REALIZAR_TRANSFERENCIA", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@CUENTA_ORIGEN", cuentaOrigen);
        command.Parameters.AddWithValue("@CUENTA_DESTINO", cuentaDestino);
        command.Parameters.AddWithValue("@IMPORTE", importe);
        command.Parameters.AddWithValue("@GLOSA", glosa);

        try
        {
            await command.ExecuteNonQueryAsync();
            return new ResultadoOperacion { Exito = true, Mensaje = "Transferencia realizada exitosamente." };
        }
        catch (SqlException ex)
        {
            return new ResultadoOperacion { Exito = false, Mensaje = ex.Message };
        }
    }

    public async Task<List<Movimiento>> ConsultarMovimientosAsync(string nroCuenta)
    {
        var movimientos = new List<Movimiento>();
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SP_CONSULTAR_MOVIMIENTOS", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };
        command.Parameters.AddWithValue("@NRO_CUENTA", nroCuenta);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            movimientos.Add(new Movimiento
            {
                NroCuenta = reader["NRO_CUENTA"].ToString()!,
                Fecha = Convert.ToDateTime(reader["FECHA"]),
                Tipo = reader["TIPO"].ToString()!,
                Importe = Convert.ToDecimal(reader["IMPORTE"]),
                TipoCambio = Convert.ToDecimal(reader["TIPO_CAMBIO"]),
                Glosa = reader["GLOSA"].ToString()!
            });
        }
        return movimientos;
    }

    // ==================== MONEDAS ====================

    public async Task<List<Moneda>> ListarMonedasAsync()
    {
        var monedas = new List<Moneda>();
        using var connection = GetConnection();
        await connection.OpenAsync();
        using var command = new SqlCommand("SELECT CODIGO, NOMBRE FROM MONEDA", connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            monedas.Add(new Moneda
            {
                Codigo = reader["CODIGO"].ToString()!,
                Nombre = reader["NOMBRE"].ToString()!
            });
        }
        return monedas;
    }
}

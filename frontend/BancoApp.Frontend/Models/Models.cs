using System.Text.Json;

namespace BancoApp.Frontend.Models;

public class Cuenta
{
    public string NroCuenta { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Moneda { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string MonedaNombre { get; set; } = string.Empty;
    public string TipoDescripcion => Tipo == "AHO" ? "CUENTA DE AHORRO" : "CUENTA CORRIENTE";
}

public class Movimiento
{
    public string NroCuenta { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal Importe { get; set; }
    public decimal TipoCambio { get; set; }
    public string Glosa { get; set; } = string.Empty;
    public string TipoDescripcion => Tipo == "D" ? "DEBITO" : "ABONO";
}

public class Moneda
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
}

public class ResultadoOperacion
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
}

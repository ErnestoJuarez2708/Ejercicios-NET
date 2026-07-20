namespace BancoApp.Backend.Models;

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

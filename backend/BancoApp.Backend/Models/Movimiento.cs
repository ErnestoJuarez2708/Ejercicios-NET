namespace BancoApp.Backend.Models;

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

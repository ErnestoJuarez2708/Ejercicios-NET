namespace BancoApp.Backend.Models;

public class ResultadoOperacion
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
}

public class CrearCuentaRequest
{
    public string NroCuenta { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string Moneda { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
}

public class MovimientoRequest
{
    public string NroCuenta { get; set; } = string.Empty;
    public decimal Importe { get; set; }
    public string Glosa { get; set; } = string.Empty;
}

public class TransferenciaRequest
{
    public string CuentaOrigen { get; set; } = string.Empty;
    public string CuentaDestino { get; set; } = string.Empty;
    public decimal Importe { get; set; }
    public string Glosa { get; set; } = string.Empty;
}

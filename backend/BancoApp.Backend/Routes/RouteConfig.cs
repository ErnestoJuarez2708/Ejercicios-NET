namespace BancoApp.Backend.Routes;

/// <summary>
/// Configuracion de rutas de la API.
/// Las rutas se definen por atributo en los controllers,
/// pero esta clase permite configurar rutas globales si es necesario.
/// </summary>
public static class RouteConfig
{
    /// <summary>
    /// Configura las rutas por defecto de la API.
    /// </summary>
    public static WebApplication ConfigureRoutes(this WebApplication app)
    {
        // Las rutas se configuran via atributo en Controllers
        // [Route("api/[controller])] en CuentasController
        // [Route("api/[controller])] en MovimientosController
        //
        // Endpoints disponibles:
        //
        // CUENTAS:
        //   GET    /api/cuentas              - Listar todas las cuentas
        //   GET    /api/cuentas/saldos       - Consultar saldos
        //   GET    /api/cuentas/{nroCuenta}  - Obtener cuenta por numero
        //   POST   /api/cuentas              - Crear cuenta
        //   GET    /api/cuentas/monedas      - Listar monedas
        //
        // MOVIMIENTOS:
        //   POST   /api/movimientos/abono        - Registrar deposito
        //   POST   /api/movimientos/retiro       - Registrar retiro
        //   POST   /api/movimientos/transferencia - Realizar transferencia
        //   GET    /api/movimientos/{nroCuenta}  - Consultar movimientos

        return app;
    }
}

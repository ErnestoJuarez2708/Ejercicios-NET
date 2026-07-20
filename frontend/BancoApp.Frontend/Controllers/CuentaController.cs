using Microsoft.AspNetCore.Mvc;

namespace BancoApp.Frontend.Controllers;

public class CuentaController : Controller
{
    public IActionResult Crear()
    {
        return View();
    }

    public IActionResult Saldos()
    {
        return View();
    }

    public IActionResult Movimientos(string nroCuenta)
    {
        ViewBag.NroCuenta = nroCuenta;
        return View();
    }
}

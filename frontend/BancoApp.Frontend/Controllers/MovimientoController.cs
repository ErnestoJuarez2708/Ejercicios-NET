using Microsoft.AspNetCore.Mvc;

namespace BancoApp.Frontend.Controllers;

public class MovimientoController : Controller
{
    public IActionResult AbonoRetiro()
    {
        return View();
    }

    public IActionResult Transferencia()
    {
        return View();
    }
}

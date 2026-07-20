using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using BancoApp.Frontend.Models;

namespace BancoApp.Frontend.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("BackendAPI");
            var response = await client.GetAsync("api/cuentas");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cuentas = JsonSerializer.Deserialize<List<Cuenta>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(cuentas ?? new List<Cuenta>());
            }
        }
        catch { }
        return View(new List<Cuenta>());
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SE310_BKTTH.Models;

namespace SE310_BKTTH.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger; // Thêm trường logger

    public HomeController(HttpClient httpClient, ILogger<HomeController> logger) // Thêm logger vào constructor
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5017/api/v1/");
        _logger = logger; 
    }

    public async Task<IActionResult> Index()
    {
        var products = await _httpClient.GetFromJsonAsync<List<ProductModel>>("Product");
        return View(products);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

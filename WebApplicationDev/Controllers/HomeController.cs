using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationDev.Models;

namespace WebApplicationDev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // логирует стандартные действия контроллера

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // методы контроллера отвечают за обработку запроса и передачу ответа в браузер пользователя

        public IActionResult Index()
        {
            // _logger.Log(LogLevel.Trace, "Index was called"); // можно делать записи логера
            this.ViewData["Text"] = "Привет";
            return View(); // View - мотод, кот конструирует объект ViewResult
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplicationDev.Models;

namespace WebApplicationDev.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // �������� ����������� �������� �����������

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ������ ����������� �������� �� ��������� ������� � �������� ������ � ������� ������������

        public IActionResult Index()
        {
            // _logger.Log(LogLevel.Trace, "Index was called"); // ����� ������ ������ ������
            this.ViewData["Text"] = "������";
            return View(); // View - �����, ��� ������������ ������ ViewResult
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

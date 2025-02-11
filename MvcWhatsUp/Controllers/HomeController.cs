using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public string SendMessage(string name, string message)
        {
            return $"Message {message} has been sent by {name}";
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(string name, string password)
        {
            return $"Logging {name} in...";
        }
    }
}

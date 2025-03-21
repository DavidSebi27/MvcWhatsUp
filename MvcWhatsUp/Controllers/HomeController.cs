using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUsersRepository usersRepository) : base(usersRepository)
        {
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
    }
}

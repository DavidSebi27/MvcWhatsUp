using Microsoft.AspNetCore.Mvc;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            List<Models.User> users =
                [
                    new Models.User(0001, "Michael Jackson", "0104", "heehee@hotmail.com", "thAtsIgnOrAnt"),
                    new Models.User(0078, "Tonald Drump", "0102", "maga@usa.com", "chinaISBAD"),
                    new Models.User(0673, "Heilon Musketeer", "0012", "definitelynotanidiotlol@teslamail.com", "TheSmartestPersonAlive"),

                ];
            
            
            return View(users);
        }
    }
}

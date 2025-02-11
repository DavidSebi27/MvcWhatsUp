using Microsoft.AspNetCore.Mvc;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            List<Models.User> users =
                [
                    new Models.User(1, "Name1", "TelephoneNum", "Email", "Passpass"),
                    new Models.User(2, "Name2", "TelephoneNum2", "Email2", "Passpass2"),
                    new Models.User(3, "Name3", "TelephoneNum3", "Email3", "Passpass3"),

                ];
            
            
            return View(users);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MvcWhatsUp.Controllers
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public string SendMessage(Models.SimpleMessage message)
        {
            return $"Message {message.MessageText} has been sent by {message.Name}";
        }
    }
}

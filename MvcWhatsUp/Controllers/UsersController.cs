using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new DummyUsersRepository();
        }
        public IActionResult Index()
        {
            List<Models.User> users = _usersRepository.GetAll();
            return View(users);
        }
    }
}

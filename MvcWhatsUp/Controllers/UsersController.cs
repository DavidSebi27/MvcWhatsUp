using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _usersRepository.Add(user);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}

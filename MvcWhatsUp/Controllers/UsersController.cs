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

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(string name, string password)
        {
            return $"Logging {name} in...";
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User? user = _usersRepository.GetById((int)id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _usersRepository.Update(user);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User? user = _usersRepository.GetById((int)id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            try
            {
                _usersRepository.Delete(user);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }
    }
}

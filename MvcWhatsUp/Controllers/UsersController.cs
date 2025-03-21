using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;

namespace MvcWhatsUp.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersRepository _usersRepository;


        public UsersController(IUsersRepository usersRepository) : base(usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IActionResult Index()
        {
            string? userIdStr = Request.Cookies["UserId"];
            int? userId = null;
            string? userName = null;

            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int id))
            {
                userId = id;
                userName = GetUserNameById(id);
            }

            ViewData["UserId"] = userId;
            ViewData["UserName"] = userName;

            List<User> users = _usersRepository.GetAll();
            return View(users);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            User? user = _usersRepository.GetLoginCredentials(loginModel.UserName, loginModel.Password);

            if (user == null)
            {
                // Login failed
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginModel);
            }
            else
            {
                // Login successful - store user ID in cookie

                // Set cookie options for security
                var cookieOptions = new CookieOptions
                {
                    // Make cookie expire in 7 days
                    Expires = DateTime.Now.AddDays(7),

                    // Prevent JavaScript access to cookie
                    HttpOnly = true,


                    // Restrict cookie to your site
                    SameSite = SameSiteMode.Strict
                };

                // Store user ID in cookie
                Response.Cookies.Append("UserId", user.UserId.ToString(), cookieOptions);

                // Redirect to index
                return RedirectToAction("Index", "Users");
            }
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

        private string GetUserNameById(int userId)
        {
            User? user = _usersRepository.GetById(userId);
            return user?.UserName ?? "Unknown Agent";
        }

        public IActionResult Profile()
        {
            // Get the user ID from the cookie
            string? userIdStr = Request.Cookies["UserId"];

            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int userId))
            {
                // If no user ID found or invalid, redirect to login
                return RedirectToAction("Login");
            }

            // Get the user details
            User? user = _usersRepository.GetById(userId);

            if (user == null)
            {
                // If user not found, redirect to login
                Response.Cookies.Delete("UserId"); // Clear invalid cookie
                return RedirectToAction("Login");
            }

            ViewData["UserName"] = user.UserName;

            return View(user);
        }

        public IActionResult Logout()
        {
            // Delete the authentication cookie
            Response.Cookies.Delete("UserId");

            // Redirect to login page
            return RedirectToAction("Login");
        }
    }
}

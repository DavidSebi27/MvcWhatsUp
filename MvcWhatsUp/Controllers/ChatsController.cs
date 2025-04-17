using Microsoft.AspNetCore.Mvc;
using MvcWhatsUp.Models;
using MvcWhatsUp.Repositories;
using MvcWhatsUp.ViewModels;

namespace MvcWhatsUp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IChatsRepository _chatsRepository;

        public ChatsController(IUsersRepository usersRepository, IChatsRepository chatsRepository)
        {
            _usersRepository = usersRepository;
            _chatsRepository = chatsRepository;
        }

        // GET: /Chats/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Chats/AddMessage/2
        [HttpGet]
        public IActionResult AddMessage(int? id)
        {
            // Receiver user id (parameter) must be available
            if (id == null)
                return RedirectToAction("Index", "Users");

            // User needs to be logged in
            // (for now, id of logged in user is stored in a cookie)
            string? loggedInUserId = Request.Cookies["UserId"];
            if (loggedInUserId == null)
                return RedirectToAction("Index", "Users");

            // Get the receiving User so we can show the name in the View
            User? receiverUser = _usersRepository.GetById((int)id);
            ViewData["ReceiverUser"] = receiverUser;

            // Create a Message object with the sender and receiver IDs
            Message message = new Message();
            message.SenderUserId = int.Parse(loggedInUserId);
            message.ReceiverUserId = (int)id;

            return View(message);
        }

        // POST: /Chats/AddMessage
        [HttpPost]
        public IActionResult AddMessage(Message message)
        {
            try
            {
                // Set the send date/time to now
                message.SendAt = DateTime.Now;

                // Add the message to the database
                _chatsRepository.AddMessage(message);

                // Confirmation
                TempData["ConfirmMessage"] = "Message sent successfully!";

                // Go to chat with the other user
                return RedirectToAction("DisplayChat", new { id = message.ReceiverUserId });
            }
            catch (Exception ex)
            {
                // Something's wrong, go back to view
                return View(message);
            }
        }

        // GET: /Chats/DisplayChat/2
        [HttpGet]
        public IActionResult DisplayChat(int? id)
        {
            // Receiver user id (parameter) must be available
            if (id == null)
                return RedirectToAction("Index", "Users");

            // User needs to be logged in
            // (for now, id of logged in user is stored in a cookie)    
            string? loggedInUserId = Request.Cookies["UserId"];
            if (loggedInUserId == null)
                return RedirectToAction("Index", "Users");

            // Get User objects via users repository
            User? sendingUser = _usersRepository.GetById(int.Parse(loggedInUserId));
            User? receivingUser = _usersRepository.GetById((int)id);
            if ((sendingUser == null) || (receivingUser == null))
                return RedirectToAction("Index", "Users");

            // Get all messages between 2 users
            List<Message> chatMessages = _chatsRepository.GetMessages(
                sendingUser.UserId, receivingUser.UserId);

            // Pass users to View via ViewModel
            ChatViewModel chatViewModel = new ChatViewModel(chatMessages, sendingUser, receivingUser);

            // Pass data to view
            return View(chatViewModel);
        }

        // GET: /Chats/SendMessage
        [HttpGet]
        public IActionResult SendMessage(int? id)
        {
            return View();
        }

        // POST: /Chats/SendMessage
        [HttpPost]
        public IActionResult SendMessage(SimpleMessage simpleMessage)
        {
            return RedirectToAction("Index", "Chats");
        }
    }
}
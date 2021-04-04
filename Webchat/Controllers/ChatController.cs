using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webchat.Bll;
using Webchat.Model;

namespace Webchat.Controllers
{
    public class ChatController : Controller
    {
        private ChatRepository chatrepo;
        private readonly ILogger<ChatController> _logger;
        private MessageRepository messageRepository;
        private UserRepository userRepository;

        public ChatController(ILogger<ChatController> logger)
        {
            _logger = logger;
            chatrepo = new ChatRepository();
            messageRepository = new MessageRepository();
            userRepository = new UserRepository();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                return View();
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [Route("/Chat/Index/{chatId:int}")]
        public IActionResult Index(int[] chatId)
        {
            int id = chatId[0];
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                ViewData["ChatId"] = id;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
                ViewData["ChatName"] = chatrepo.Get(id).ChatName;
                List<Message> messages = messageRepository.GetByFilter(m => m.ChatId == id);
                List<string> senders = new List<string>();
                Dictionary<Message, string> messagewithusername = new Dictionary<Message, string>();

                foreach (Message m in messages)
                {
                    messagewithusername.Add(m,userRepository.Get(m.Sender).UserName);
                }
                ViewData["Messages"] = messagewithusername;
                return View();
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [Route("/Chat/GoChat/{chatId:int}")]
        public IActionResult GoChat(int[] chatId)
        {
            int id = chatId[0];
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                ViewData["ChatId"] = id;
                ViewData["UserName"] = HttpContext.Session.GetString("UserName");
                ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
                ViewData["ChatName"] = chatrepo.Get(id).ChatName;
                List<Message> messages = messageRepository.GetByFilter(m => m.ChatId == id);
                List<string> senders = new List<string>();
                Dictionary<Message, string> messagewithusername = new Dictionary<Message, string>();

                foreach (Message m in messages)
                {
                    messagewithusername.Add(m, userRepository.Get(m.Sender).UserName);
                }
                ViewData["Messages"] = messagewithusername;
                return View("~/Views/Chat/Index.cshtml");
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            Console.WriteLine(TempData["Message"].ToString());
            return RedirectToAction("Index");
        }

    }
}

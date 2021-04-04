using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Webchat.Bll;
using Webchat.Model;
using Webchat.Models;


namespace Webchat.Models
{
    public class DashboardController : Controller
    {
        private ChatRepository chatrepo;
        private UserRepository userrepo;
        private ChatUserRepository chatuserrepo;
        private readonly ILogger<DashboardController> _logger;
        private User user;
        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
            userrepo = new UserRepository();
            chatrepo = new ChatRepository();
            chatuserrepo = new ChatUserRepository();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                return View();
            }
            return View("~/Views/Home/Index.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult CreateChat()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                List<User> Users = userrepo.GetAll();
                Users.Remove(userrepo.GetByMail(HttpContext.Session.GetString("UserMail")));
                ViewData["Users"] = Users;
                return View();
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            Console.WriteLine(TempData["Message"].ToString());
            return RedirectToAction("Index");
        }
        
        public IActionResult DeleteChat(int[] chatId)
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                int ChatId = chatId[0];
                List<ChatUser> result = chatuserrepo.GetByFilter(x => x.ChatId == ChatId);
                foreach (ChatUser x in result)
                {
                    chatuserrepo.Delete(x.ChatUserId);
                    chatrepo.Delete(x.ChatId);
                }
                return View("Index");
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            Console.WriteLine(TempData["Message"].ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Done(string[] users)
        {
            if (users.Length > 0)
            {
                if (HttpContext.Session.GetString("UserMail") != null)
                {
                    string name = "";
                    foreach (var user in users)
                    {
                        name += userrepo.GetByMail(user).UserName;
                        name += " ";
                    }
                    Chat chat = new Chat { ChatCreated = DateTime.Now, ChatName = name };
                    chatrepo.Add(chat);
                    string result = chatuserrepo.Add(new ChatUser { UserId = userrepo.GetByMail(HttpContext.Session.GetString("UserMail")).UserId, ChatId = chat.ChatId });
                    Console.WriteLine(result);
                    foreach (var user in users)
                    {
                        ChatUser chatuser = new ChatUser { UserId = userrepo.GetByMail(user).UserId, ChatId = chat.ChatId };
                        result = chatuserrepo.Add(chatuser);
                        Console.WriteLine(result);
                    }
                    return View("~/Views/Home/Index.cshtml");
                }
                TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
                Console.WriteLine(TempData["Message"].ToString());
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}

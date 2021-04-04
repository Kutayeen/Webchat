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

namespace Webchat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ChatUserRepository chatuserrepo;
        private UserRepository userrepo;
        private User user;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            userrepo = new UserRepository();
            chatuserrepo = new ChatUserRepository();
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                user = userrepo.GetByMail(HttpContext.Session.GetString("UserMail"));
                if (user != null)
                {
                    List<ChatUser> ChatUser = chatuserrepo.GetByUser(user.UserId);
                    List<Chat> chats = new List<Chat>();
                    foreach (ChatUser chat in ChatUser)
                    {
                        chats.Add(new ChatRepository().Get(chat.ChatId));
                    }
                    if(chats.Count>0)
                    ViewData["Chats"] = chats;
                    Console.WriteLine(ChatUser.Count);
                    return View("~/Views/Dashboard/Index.cshtml");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                return View("~/Views/Dashboard/Index.cshtml");
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            return RedirectToAction("Index");
        }

        public IActionResult CreateChat()
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                return View();
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Dashboard(UserViewModel model)
        {
            if (model.UserMail != null && model.UserPassword != null)
            {

                user = userrepo.Login(model.UserMail, model.UserPassword);
                TempData["Message"] = user != null ? new string[] { "alert-success", "User logged in." } :
                    new string[] { "alert-danger", "Something went wrong!" };
                if (user != null)
                {
                    HttpContext.Session.SetString("UserMail", user.UserMail);
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("UserPassword", user.UserPassword);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    if (user.IsAdmin)
                    {
                        HttpContext.Session.SetString("IsAdmin", "true");
                    }
                    else
                    {
                        HttpContext.Session.SetString("IsAdmin", "false");

                    }
                    List<ChatUser> ChatUser = chatuserrepo.GetByUser(user.UserId);
                    List<Chat> chats = new List<Chat>();
                    foreach (ChatUser chat in ChatUser)
                    {
                        chats.Add(new ChatRepository().Get(chat.ChatId));
                    }
                    if (chats.Count > 0)
                        ViewData["Chats"] = chats;
                    return View("~/Views/Dashboard/Index.cshtml");
                }
            }
            else
            {
                TempData["Message"] = new string[] { "alert-danger", "You must fill all fields." };
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (model.UserMail != null && model.UserPassword != null && model.UserPasswordAgain != null)
            {
                if (!model.UserPasswordAgain.Equals(model.UserPassword))
                {
                    TempData["Message"] = new string[] { "alert-danger", "Passwords aren't matched." };
                }
                else if (model.UserPassword.Length > 50 || model.UserPassword.Length < 5)
                {
                    TempData["Message"] = new string[] { "alert-danger", "Passwords must be between 5 and 50 characters." };
                }
                else
                {
                    Model.User user = userrepo.Register(model.UserName, model.UserMail, model.UserPassword, null);
                    TempData["Message"] = user != null ? new string[] { "alert-success", "User registered." } :
                        new string[] { "alert-danger", "Email is already used." };
                }
            }
            else
            {
                TempData["Message"] = new string[] { "alert-danger", "You must fill all fields." };
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

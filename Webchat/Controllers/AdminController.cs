using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webchat.Bll;
using Webchat.Model;
using Webchat.Models;

namespace Webchat.Controllers
{
    public class AdminController : Controller
    {
        private UserRepository userrepo = new UserRepository();
        private User user;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserMail") != null && HttpContext.Session.GetString("IsAdmin").Equals("true"))
            {
                if (HttpContext.Session.GetString("UserMail") != null)
                {
                    List<User> Users = userrepo.GetAll();
                    ViewData["Users"] = Users;
                    return View();
                }
                TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
                Console.WriteLine(TempData["Message"].ToString());
                return RedirectToAction("Index");
            }
            return View("~/Views/Home/Index.cshtml");
        }
        [Route("/Admin/UserDetail/{userId:int}")]
        public IActionResult UserDetail(int userId)
        {
            if (HttpContext.Session.GetString("UserMail") != null && HttpContext.Session.GetString("IsAdmin").Equals("true"))
            {
                
                user = userrepo.Get(userId);
                ViewData["UserName"] = user.UserName;
                ViewData["UserMail"] = user.UserMail;
                ViewData["IsUserAdmin"] = user.IsAdmin ? "true" : "false";
                Console.WriteLine(ViewData["IsUserAdmin"]);

                return View("~/Views/Admin/UserDetail.cshtml");
            }
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult UserDetail()
        {
           
            return View("~/Views/Home/Dashboard.cshtml");
        }

        public IActionResult Save(UserViewModel model)
        {
            if (model.UserName != null)
            {
                user = userrepo.GetByMail(model.UserMail);                
                user.IsAdmin = model.IsAdmin !=null ? true: false;
                user.UserMail = model.UserMail;
                user.UserName = model.UserName;
                userrepo.Update(user);
            }
            else
            {
                TempData["Message"] = new string[] { "alert-danger", "You must fill all fields." };
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create(UserViewModel model)
        {
            
            if (model.UserName != null && model.UserMail != null && model.UserPassword != null)
            {
                Console.WriteLine(model.IsAdmin);
                Console.WriteLine(model.UserMail);
                Console.WriteLine(model.UserName);
                userrepo.Register(model.UserName, model.UserMail,model.UserPassword,null);
            }
            else
            {
                TempData["Message"] = new string[] { "alert-danger", "You must fill all fields." };
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateUser()
        {
            if (HttpContext.Session.GetString("UserMail") != null && HttpContext.Session.GetString("IsAdmin").Equals("true"))
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int UserId)
        {
            if (HttpContext.Session.GetString("UserMail") != null)
            {
                userrepo.Delete(UserId);
                return RedirectToAction("Index");
            }
            TempData["Message"] = new string[] { "alert-danger", "Please, login..." };
            Console.WriteLine(TempData["Message"].ToString());
            return RedirectToAction("Index");
        }
    }
}

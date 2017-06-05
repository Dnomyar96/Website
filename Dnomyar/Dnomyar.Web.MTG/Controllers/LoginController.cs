using Dnomyar.Web.MTG.Helpers;
using Dnomyar.Web.MTG.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dnomyar.Web.MTG.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string message = "", string username = "")
        {
            var model = new LoginVM()
            {
                Error = message,
                UserName = username
            };

            return View(model);
        }

        public ActionResult Login(LoginVM model)
        {
            if(string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                return RedirectToAction("Index", new { message = "Username and password are required", username = model.UserName });

            CookieHelper.CreateUserCookie(0);
            return RedirectToAction("Index");
        }
    }
}
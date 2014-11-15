using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ManagementTool.Framework.Mediators;
using ManagementTool.Framework.Models.Account;

namespace ManagementTool.Website.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return Login();
        }

        [HttpGet]
        public ActionResult Login()
        {
            var loginModel = new LoginVM();
            return View("~/Views/Account/Login.cshtml", loginModel);
        }

        [HttpGet]
        public ActionResult Register()
        {
            var registerModel = new RegisterVM();
            return View("~/Views/Account/Register.cshtml", registerModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM user)
        {
            UserMediator mediator = new UserMediator();
            if (user.Password != user.VerifyPassword)
                ModelState.AddModelError("ErrorMessage", "Passwords don't match");


            if (ModelState.IsValid)
            {
                bool success = mediator.RegisterUser(user);

                if (success)
                    return Redirect("/");
                else
                    ModelState.AddModelError("ErrorMessage", "Unable to create account");
                
            }

            return PartialView("~/Views/Account/Register.cshtml", user);
        }

        [HttpPost]
        public ActionResult Login(LoginVM user)
        {
            UserMediator mediator = new UserMediator();

            var isAuthenticated = mediator.Authenticate(user);
            if (isAuthenticated)
            {
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(30), false, "");
                string CookieContents = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, CookieContents) { Path = FormsAuthentication.FormsCookiePath };
                if (HttpContext.Response != null)
                {
                    HttpContext.Response.Cookies.Add(cookie);
                }

                Session["UserAccount"] = mediator.GetUser(user.Email);

                return Redirect("~/project/index");
            }
            else
            {
                ModelState.AddModelError("ErrorMessage", "Invalid credentials");
            }

            return PartialView("~/Views/Account/Login.cshtml", user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

    }
}

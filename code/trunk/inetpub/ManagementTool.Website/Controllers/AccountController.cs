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

        public ActionResult Login()
        {
            var loginModel = new LoginVM();
            return View("~/Views/Account/Login.cshtml", loginModel);
        }

        public ActionResult Register()
        {
            var registerModel = new RegisterVM();
            return View("~/Views/Account/Register.cshtml", registerModel);
        }

        public ActionResult CreateUser(RegisterVM user)
        {
            UserMediator mediator = new UserMediator();

            mediator.RegisterUser(user);

            return Redirect("/");
        }

        public ActionResult SignIn(LoginVM user)
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

                return Redirect("~/account/login");
            }

            return Redirect("/");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }

    }
}

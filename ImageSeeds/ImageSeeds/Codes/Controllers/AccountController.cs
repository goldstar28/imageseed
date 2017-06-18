using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ImageSeeds.Codes.Dtos;
using ImageSeeds.Codes.Entities;
using ImageSeeds.Codes.Services;

namespace ImageSeeds.Codes.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserService _userService = new UserService();
        // GET: Account
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (!ModelState.IsValid) return View(user);
            var users = _userService.Listuser();
            if (
                users.Any(
                    x =>
                        string.Equals(x.UserName.Trim(), user.UserName.Trim(), StringComparison.CurrentCultureIgnoreCase)))
            {
                ModelState.AddModelError("", "Username already taken");
                return View(user);
            }
            _userService.InsertUser(user);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDto obj, string returnUrl)
        {
            if (!ModelState.IsValid) return View(obj);
            var users =
                _userService.Listuser()
                    .Where(
                        x =>
                            string.Equals(x.UserName.Trim(), obj.UserName.Trim(),
                                StringComparison.CurrentCultureIgnoreCase) && x.Password == obj.Password)
                    .ToList();
            if (users.Any())
            {
                var user = users.First();
                FormsAuthentication.SetAuthCookie(user.Id + "|" + user.UserName, false);
                if (returnUrl != null && returnUrl.Length > 1)
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid credentials");
            return View(obj);
        }

        public ActionResult Logout()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
using Core.Interface;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IlseLeijten.Controllers
{
    public class AccountController : Controller
    {
        private IMembershipService _service;
        private IAuthorizedUserManager _authorizedUserManager;

        public AccountController(IMembershipService service, IAuthorizedUserManager authorizedUserManager)
        {
            _service = service;
            _authorizedUserManager = authorizedUserManager;
        }

        public ActionResult Authenticate()
        {
            return (Request.UrlReferrer != null) ? 
                Login("https://www.google.com/accounts/o8/id") :
                Login();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var user = _service.GetUser();

            if (user != null)
            {
                if (user.IsSignedByProvider && _authorizedUserManager.IsAllowedUser(user.Email))
                {
                    var cookie = _service.CreateFormsAuthenticationCookie(user);
                    HttpContext.Response.Cookies.Add(cookie);

                    return new RedirectResult(Request.Params["ReturnUrl"] ?? "/");
                }
            }

            // if user is not allowed to login, redirect back to home page
            return new RedirectResult("/");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string openid_identifier)
        {
            return _service.RedirectToAuthenticationProvider(openid_identifier);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            _service.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}

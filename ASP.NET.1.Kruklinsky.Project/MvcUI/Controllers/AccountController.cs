using MvcUI.Models;
using MvcUI.Providers;
using MvcUI.Providers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            this.authProvider = authProvider;
        }

        [NoAuthorize]
        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.LogIn(model.Email, model.Password, true))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            authProvider.LogOut();
            return RedirectToAction("Index", "Home");
        }

        [NoAuthorize]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                string errorMessage;
                if (authProvider.SingUp(model.Email, model.Password, isApproved: false,error: out errorMessage))
                {
                    if (Url != null)
                    {
                        return RedirectToAction("Index", "Verify", new ActivateModel { Email = model.Email+"/" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                }
            }
            return View(model);
        }

        [NoAuthorize]
        [HttpPost]
        public JsonResult IsFreeEmail(string email)
        {
            bool result = false;
            MvcUIMembershipProvider provider = (Membership.Provider as MvcUIMembershipProvider);
            if (provider != null)
            {
                result = !provider.IsDuplicateEmail(email);
            }
            return Json(result);
        }

    }
}

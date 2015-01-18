using MvcUI.Models;
using MvcUI.Providers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Controllers
{
    [NoAuthorize]
    public class VerifyController : Controller
    {
        private IVerifyProvider verifyProvider;

        public VerifyController(IVerifyProvider verifyProvider)
        {
            this.verifyProvider = verifyProvider;
        }

        [OutputCache(Duration = 60)]
        public ActionResult Index(ActivateModel model)
        {
            if (!String.IsNullOrWhiteSpace(model.Email))
            {
                if (this.verifyProvider.IsEmail(model.Email) && this.verifyProvider.IsVerifying(model.Email))
                {
                    this.verifyProvider.SendVerifyEmail(model.Email);
                    MailAddress mailAddress = new MailAddress(model.Email);
                    if (mailAddress != null)
                    {
                        ViewBag.userHost = mailAddress.Host;
                        ViewBag.userEmail = model.Email;
                        return View();
                    }
                }
            }
            return RedirectToAction("LogIn", "Account");
        }

        public ActionResult Verify(ActivateModel model)
        {
            if (!String.IsNullOrWhiteSpace(model.Email) && !String.IsNullOrWhiteSpace(model.SecretCode))
            {
                if (this.verifyProvider.IsEmail(model.Email) && this.verifyProvider.IsVerifying(model.Email))
                {
                    if (verifyProvider.Verify(model.Email, model.SecretCode))
                    {
                        return RedirectToAction("LogIn", "Account");
                    }
                }
            }
            return RedirectToAction("SignUp", "Account");
        }
    }
}

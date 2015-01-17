using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcUI.Controllers
{
    public class NoAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                throw new InvalidOperationException("Authorize attribute cannot use within child action cache.");
            }
            if (!this.AuthorizeCore(filterContext.HttpContext))
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Index");
                redirectTargetDictionary.Add("controller", "Home");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }
    }
}
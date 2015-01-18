using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RegisterAccountRoutes(routes);
            RegisterVerifyRoutes(routes);

            RegisterAdminRoutes(routes);



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(null, "{controller}/{action}");
        }

        private static void RegisterAccountRoutes(RouteCollection routes)
        {
            routes.MapRoute(null, "SignUp", new { controller = "Account", action = "SignUp" });
            routes.MapRoute(null, "Account/SignUp", new { controller = "Account", action = "SignUp" });
            routes.MapRoute(null, "Registration", new { controller = "Account", action = "SignUp" });
            routes.MapRoute(null, "Register", new { controller = "Account", action = "SignUp" });
            routes.MapRoute(null, "LogIn", new { controller = "Account", action = "LogIn" });
            routes.MapRoute(null, "Account/LogOn", new { controller = "Account", action = "LogIn" });
            routes.MapRoute(null, "LogOn", new { controller = "Account", action = "LogIn" });
        }

        private static void RegisterVerifyRoutes(RouteCollection routes)
        {
            routes.MapRoute(null, "Verify/{Email}/", new { controller = "Verify", action = "Index" });
            routes.MapRoute(null, "Verify/Verify/{Email}/{SecretCode}", new { controller = "Verify", action = "Verify" });
        }

        private static void RegisterAdminRoutes(RouteCollection routes)
        {
            routes.MapRoute(null, "Admin/EditTest{testId}", new { controller = "Admin", action = "EditTest" }, new { testId = @"\d+" });
            routes.MapRoute(null, "Admin/EditTest/{testId}", new { controller = "Admin", action = "EditTest" }, new { testId = @"\d+" });
            routes.MapRoute(null, "Admin/EditQuestion{questionId}", new { controller = "Admin", action = "EditQuestion" }, new { questionId = @"\d+" });
            routes.MapRoute(null, "Admin/EditQuestion/{questionId}", new { controller = "Admin", action = "EditQuestion" }, new { questionId = @"\d+" });
        }
    }
}
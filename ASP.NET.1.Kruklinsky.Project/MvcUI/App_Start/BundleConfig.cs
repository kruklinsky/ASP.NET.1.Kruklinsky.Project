using System.Web;
using System.Web.Optimization;

namespace MvcUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Content/admin/js").Include("~/Content/admin/admin.js"));

            bundles.Add(new ScriptBundle("~/Content/home/js").Include("~/Content/stopwatch.js"));
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include("~/Content/bootstrap/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/error/css").Include("~/Content/error/error.css"));
            bundles.Add(new StyleBundle("~/Content/signup/css").Include("~/Content/signup/signup.css"));
            bundles.Add(new StyleBundle("~/Content/verify/css").Include("~/Content/verify/verify.css"));
            bundles.Add(new StyleBundle("~/Content/login/css").Include("~/Content/login/login.css"));
            bundles.Add(new StyleBundle("~/Content/home/css").Include("~/Content/home/home.css"));
            bundles.Add(new StyleBundle("~/Content/result/css").Include("~/Content/result/result.css"));
            bundles.Add(new StyleBundle("~/Content/admin/css").Include("~/Content/admin/admin.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
using log4net;
using MvcUI.Filters;
using System.Web;
using System.Web.Mvc;

namespace MvcUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Log4netHandleErrorAttribute(LogManager.GetLogger("log4net-error.txt")));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace MvcUI.Filters
{
    public class Log4netHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly ILog logger;

        public Log4netHandleErrorAttribute(ILog logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger", "Logger is null.");
            }
            this.logger = logger;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext","Filter context is null.");
            }
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }
            if (!this.ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }
            if (IsAjax(filterContext))
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                base.OnException(filterContext);
            }
            logger.Error(filterContext.Exception.Message, filterContext.Exception);
        }

        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
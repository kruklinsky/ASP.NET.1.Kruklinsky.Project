using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.HtmlHelpers
{
    public static class Helpers
    {
        public static MvcHtmlString HideImage(this HtmlHelper html, int hideId)
        {
            TagBuilder result = new TagBuilder("img");
            result.MergeAttribute("onclick", "hide(" + hideId + ")&changeImage(jQuery.event.fix(event).target)");
            result.MergeAttribute("src", "/Content/admin/images/hide.png");
            result.MergeAttribute("width", "15");
            result.MergeAttribute("height", "15");
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
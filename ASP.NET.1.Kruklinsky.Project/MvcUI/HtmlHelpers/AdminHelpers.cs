using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcUI.HtmlHelpers
{
    public static class AdminHelpers
    {
        public static MvcHtmlString TestQuestions(this HtmlHelper html, IEnumerable<Question> questions, int hideId, string label)
        {
            return MvcHtmlString.Create(Hider(label, hideId, DisplayForQuestions(html, questions)));
        }

        private static string DisplayForQuestions(HtmlHelper html, IEnumerable<Question> questions)
        {
            StringBuilder result = new StringBuilder();
            foreach (var question in questions)
            {
                result.Append(html.ActionLink(question.Text.Substring(0,50)+"...", "EditQuestion", "Admin", new { questionId = question.Id }, null));
                result.Append("<br /> <br />");
            }
            return result.ToString();
        }

        #region Hider

        public static string Hider(string label, int hideId, string innerHtml)
        {
            StringBuilder result = new StringBuilder();
            result.Append(Image(hideId));
            result.Append(Label(label));
            result.Append(InnerHtml(hideId, innerHtml));
            return result.ToString();
        }

        public static string Image(int hideId)
        {
            TagBuilder result = new TagBuilder("img");
            result.MergeAttribute("onclick", "hide(" + hideId + ")&changeImage(jQuery.event.fix(event).target)");
            result.MergeAttribute("src", "/Content/admin/images/hide.png");
            result.MergeAttribute("width", "15");
            result.MergeAttribute("height", "15");
            return result.ToString();
        }
        public static string Label(string label)
        {
            TagBuilder result = new TagBuilder("label");
            result.InnerHtml = label;
            return result.ToString();
        }
        public static string InnerHtml (int hideId, string innerHtml)
        {
            TagBuilder result = new TagBuilder("div");
            result.MergeAttribute("id", hideId.ToString());
            result.InnerHtml = innerHtml;
            return result.ToString();
        }

        #endregion
    }
}
using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace MvcUI.HtmlHelpers
{
    public static class HomeHelpers
    {
        public static MvcHtmlString DisplayForAnswers(this HtmlHelper html, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            List<string> result = new List<string>();
            result.AddRange(answers.Select(a => a.Text));
            result.AddRange(fakes.Select(f => f.Text));
            if (answers.Count() > 1)
            {
                return MvcHtmlString.Create(Answers(result, RadionAnswer));
            }
            else
            {
                return MvcHtmlString.Create(Answers(result, CheckAnswer));
            }
        }

        public static string Answers(List<string> answers, Func<string, string> getinput)
        {
            StringBuilder result = new StringBuilder();
            foreach (var answer in answers)
            {
                result.Append(getinput(answer));
            }
            return result.ToString();
        }

        public static string CheckAnswer (string innerHtml)
        {
            TagBuilder result = new TagBuilder("lable");
            result.AddCssClass("checkbox");
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type","checkbox");
            input.MergeAttribute("value","");
            result.InnerHtml = input.ToString() + innerHtml;
            return result.ToString();
        }

        public static string RadionAnswer(string innerHtml)
        {
            TagBuilder result = new TagBuilder("lable");
            result.AddCssClass("radion");
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "radion");
            input.MergeAttribute("name", "optionsRadios");

            result.InnerHtml = input.ToString() + innerHtml;
            return result.ToString();
        }
    }
}
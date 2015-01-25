using MvcUI.Infrastructure.Abstract;
using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Binders
{
    public class TestingModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(Testing))
            {
                var request = controllerContext.HttpContext.Request;
                var cart = (ITestSession)controllerContext.HttpContext.Session["TestSession"];
                return this.GetUserAnswers(request, cart);
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }
        }

        private IEnumerable<Answers> GetUserAnswers(HttpRequestBase request, ITestSession testSession)
        {
            var result = new List<Answers>();
            if (testSession != null && testSession.IsStarted && testSession.Test != null)
            {
                for (int i = 0; i < testSession.Test.Answers.Count(); i++)
                {
                    result.Add(this.GetUserAnswer(request, testSession, i));
                }
            }
            return result;
        }
        private Answers GetUserAnswer(HttpRequestBase request, ITestSession testSession, int i)
        {
            Answers result = new Answers();
            for (int j = 0; j < testSession.Test.Answers[i].UserAnswers.Count(); j++)
            {
                string userAnser = request.Form["Answers[" + i + "].UserAnswers[" + j + "].UserAnswer"];
                result.UserAnswers.Add(new AnswerPair { UserAnswer = userAnser != "false"});
            }
            return result;
        }
    }
}
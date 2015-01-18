using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.Models;
using System.Web.Security;

namespace MvcUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IKnowledgeService knowledgeService;
        private ITestService testService;

        public HomeController(IKnowledgeService knowledgeService, ITestService testService)
        {
            if (knowledgeService == null)
            {
                throw new System.ArgumentNullException("knowledgeService", "Knowledge service is null.");
            }
            if (testService == null)
            {
                throw new System.ArgumentNullException("testService", "Test service service is null.");
            }
            this.knowledgeService = knowledgeService;
            this.testService = testService;
        }

        public ActionResult Index()
        {
            var subjects = knowledgeService.GetAllSubjects();
            var model = new List<SubjectEditor>(
                subjects.Select(s =>
                    new SubjectEditor
                    {
                        Subject = s.ToWeb(),
                        Tests = s.Tests.Value.Select(t => t.ToWeb())
                    }));
            return View(model);
        }

        public ActionResult Test(int testId)
        {
            if (testId > 0)
            {
                var test = knowledgeService.GetTest(testId);
                if (test != null)
                {
                    return View(test.ToWeb());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StartTest(int testId)
        {
            if (!this.GetCart().IsStarted)
            {
                var test = knowledgeService.GetTest(testId);
                if (test != null)
                {
                    Testing onTest = new Testing
                    {
                        Test = test.ToWeb(),
                        Questions = new List<QuestionEditor>(test.Questions.Value.Select(q =>
                            new QuestionEditor
                            {
                                Question = q.ToWeb(),
                                Answers = q.Answers.Value.Select(a => a.ToWeb()).ToList(),
                                Fakes = q.Fakes.Value.Select(f => f.ToWeb()).ToList()
                            }))
                    };
                    string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                    int resultId = this.testService.StartTest(userId, testId, 1200);
                    this.GetCart().Start(onTest, resultId);
                }
            }
            return RedirectToAction("Testing", "Home");
        }

        public ActionResult Testing()
        {
            if (this.GetCart().IsStarted)
            {
                return View(this.GetCart().Test);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Testing(Answers answers)
        {
            if (this.GetCart().IsStarted)
            {
                this.testService.FinishTest(this.GetCart().ResultId, this.GetCart().Finish(Request.Form));
                this.DisposeCart();
            }
            return RedirectToAction("Index", "Home");
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void DisposeCart()
        {
            Session["Cart"] = null;
        }
    }
}

using BLL.Interface.Abstract;
using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcUI.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        private IKnowledgeService knowledgeService;
        private ITestService testService;

        public ResultController(IKnowledgeService knowledgeService, ITestService testService)
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
            string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var results = this.testService.GetUserResults(userId);
            var tests = new List<BLL.Interface.Entities.Test>();
            foreach(var result in results)
            {
                tests.Add(this.knowledgeService.GetTest(result.TestId));
            }
            Results model = new Results { Result = results.Select(r => r.ToWeb()).ToList(), Tests = tests.Select(t => t.ToWeb()).ToList() };
            return View(model);
        }

    }
}

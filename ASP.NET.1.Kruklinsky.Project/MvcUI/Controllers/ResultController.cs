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
        private ITestQueryService testQueryService;
        private IResultQueryService resultQueryService;
        private ITestingService testingService;

        public ResultController(ITestingService testingService, IResultQueryService resultQueryService, ITestQueryService testQueryService)
        {
            if(resultQueryService == null)
            {
                throw new System.ArgumentNullException("resultQueryService", "Result query service service is null.");
            }
            if (testingService == null)
            {
                throw new System.ArgumentNullException("testService", "Test service service is null.");
            }
            this.testQueryService = testQueryService;
            this.resultQueryService = resultQueryService;
            this.testingService = testingService;
        }

        public ActionResult Index()
        {
            string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var results = this.resultQueryService.GetUserResults(userId);
            var tests = new List<BLL.Interface.Entities.Test>();
            foreach(var result in results)
            {
                tests.Add(this.testQueryService.GetTest(result.TestId));
            }
            Results model = new Results { Result = results.Select(r => r.ToWeb()).ToList(), Tests = tests.Select(t => t.ToWeb()).ToList() };
            return View(model);
        }

    }
}

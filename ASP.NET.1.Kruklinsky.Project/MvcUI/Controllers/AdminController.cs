using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Abstract;
using MvcUI.Models;

namespace MvcUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IKnowledgeService knowledgeService;

        public AdminController(IKnowledgeService knowledgeService)
        {
            this.knowledgeService = knowledgeService;
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var subjects = knowledgeService.GetAllSubjects();
            var model = new Subjects { Data = subjects.Select(s => new Subject { subject = s, Tests = s.Tests.Value }) };
            return View(model);
        }

    }
}

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
            var model = new Subjects { Data = subjects.Select(s => new SubjectEditor { Subject = s.ToWeb(), Tests = s.Tests.Value.Select(t => t.ToWeb()).ToList() }) };
            return View(model);
        }

        #region Subject

        public ActionResult AddSubject()
        {
            var model = new Subject();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                this.knowledgeService.CreateSubject(subject.Name, subject.Description);
                return RedirectToAction("Index", "Admin");
            }
            return View(subject);
        }
        public ActionResult EditSubject(int subjectId)
        {
            if (subjectId > 0)
            {
                var subject = knowledgeService.GetSubject(subjectId);
                if (subject != null)
                {
                    var model = new SubjectEditor
                    {
                        Subject = subject.ToWeb(),
                        Tests = subject.Tests.Value.Select(t => t.ToWeb()).ToList()
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult EditSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (subject.Id > 0)
                {
                    this.knowledgeService.UpdateSubject(subject.ToBll());
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(subject);
        }

        #endregion

        #region Test

        public ActionResult AddTest(int subjectId)
        {
            var model = new Test { SubjectId = subjectId };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddTest(Test test)
        {
            if (ModelState.IsValid)
            {
                this.knowledgeService.CreateTest(test.SubjectId, test.Name, test.Topic, test.Description);
                return RedirectToAction("EditSubject", "Admin", new { subjectId = test.SubjectId });
            }
            return View(test);
        }
        public ActionResult EditTest(int testId)
        {
            if (testId > 0)
            {
                var test = knowledgeService.GetTest(testId);
                if (test != null)
                {
                    var model = new TestEditor
                    {
                        Test = test.ToWeb(),
                        Questions = test.Questions.Value.Select(q => q.ToWeb()).ToList()
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult EditTest(Test test)
        {
            if (ModelState.IsValid)
            {
                if (test.Id > 0)
                {
                    this.knowledgeService.UpdateTest(test.ToBll());
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(test);
        }

        #endregion

        #region Question

        public ActionResult AddQuestion(int testId, int subjectId)
        {
            var model = new Question { TestId = testId, SubjectId = subjectId};
            return View(model);
        }
        [HttpPost]
        public ActionResult AddQuestion (Question question)
        {
            if (ModelState.IsValid)
            {
                this.knowledgeService.AddNewTestQuestions(question.TestId, new List<BLL.Interface.Entities.Question> { question.ToBll() });
                return RedirectToAction("EditTest", "Admin", new { testId = question.TestId });
            }
            return View(question);
        }
        public ActionResult EditQuestion(int questionId)
        {
            if (questionId > 0)
            {
                var question = knowledgeService.GetQuestion(questionId);
                if (question != null)
                {
                    var model = new QuestionEditor
                    {
                        Question  = question.ToWeb(),
                        Answers = question.Answers.Value.Select(a=> a.ToWeb()).ToList(),
                        Fakes = question.Fakes.Value.Select(f => f.ToWeb()).ToList()
                    };

                        return View(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult EditQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                if (question.Id > 0)
                {
                    this.knowledgeService.UpdateQuestion(question.ToBll());
                }
                return RedirectToAction("EditQuestion", "Admin", new { questionId = question.Id });
            }
            return View(question);
        }

        #endregion

        #region Answer

        public ActionResult AddAnswer(int questionId)
        {
            var model = new Answer { QuestionId = questionId };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                this.knowledgeService.AddNewQuestionAnswers(answer.QuestionId, new List<BLL.Interface.Entities.Answer> { answer.ToBll() });
                return RedirectToAction("EditQuestion", "Admin", new { questionId = answer.QuestionId });

            }
            return View(answer);
        }

        public ActionResult EditAnswer(int answerId)
        {
            if (answerId > 0)
            {
                var answer = knowledgeService.GetAnswer(answerId);
                if (answer != null)
                {
                    var model = answer.ToWeb();
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult EditAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                if (answer.Id > 0)
                {
                    this.knowledgeService.UpdateAnswer(answer.Id,answer.Text);
                }
                return RedirectToAction("EditAnswer", "Admin", new { answerId = answer.Id });
            }
            return View(answer);
        }

        #endregion

        #region Fake

        public ActionResult AddFake(int questionId)
        {
            var model = new Fake { QuestionId = questionId };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddFake(Fake fake)
        {
            if (ModelState.IsValid)
            {
                this.knowledgeService.AddNewQuestionFakes(fake.QuestionId, new List<BLL.Interface.Entities.Fake> { fake.ToBll() });
                return RedirectToAction("EditQuestion", "Admin", new { questionId = fake.QuestionId });

            }
            return View(fake);
        }

        public ActionResult EditFake(int fakeId)
        {
            if (fakeId > 0)
            {
                var fake = knowledgeService.GetFake(fakeId);
                if (fake != null)
                {
                    var model = fake.ToWeb();
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Admin");
        }
        [HttpPost]
        public ActionResult EditFake(Fake fake)
        {
            if (ModelState.IsValid)
            {
                if (fake.Id > 0)
                {
                    this.knowledgeService.UpdateFake(fake.Id, fake.Text);
                }
                return RedirectToAction("EditFake", "Admin", new { fakeId = fake.Id });
            }
            return View(fake);
        }

        #endregion

    }
}

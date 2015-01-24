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
        private ITestQueryService testQueryService;
        private ITestCreationService testCreationService;
        private ITestQuestionsManagementService testQuestionsManagementService;

        private IQuestionAnswersManagmentService questionAnswersManagmentService;
        private IAnswersQueryService answersQueryService;

        private IQuestionCreationService questionCreationService;
        private IQuestionQueryService questionQueryService;


        private ISubjectCreationService subjectCreationService;
        private ISubjectQueryService subjectQueryService;


        public AdminController(
            ISubjectCreationService subjectCreationService, 
            ISubjectQueryService subjectQueryService,
            IQuestionAnswersManagmentService questionAnswersManagmentService,
            IAnswersQueryService answersQueryService,
            IQuestionCreationService questionCreationService,
            IQuestionQueryService questionQueryService,
            ITestQueryService testQueryService,
            ITestCreationService testCreationService,
            ITestQuestionsManagementService testQuestionsManagementService
            )
        {

            this.subjectCreationService = subjectCreationService;
            this.subjectQueryService = subjectQueryService;
            this.questionAnswersManagmentService = questionAnswersManagmentService;
            this.answersQueryService = answersQueryService;
            this.questionCreationService = questionCreationService;
            this.questionQueryService = questionQueryService;
            this.testQueryService = testQueryService;
            this.testCreationService = testCreationService;
            this.testQuestionsManagementService = testQuestionsManagementService;
        }

        public ActionResult Index()
        {
            var subjects = this.subjectQueryService.GetAllSubjects();
            var model = new Subjects { Data = subjects.Select(s => new SubjectEditor { Subject = s.ToWeb(), Tests = s.Tests.Select(t => t.ToWeb()).ToList() }) };
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
                this.subjectCreationService.CreateSubject(subject.Name, subject.Description);
                return RedirectToAction("Index", "Admin");
            }
            return View(subject);
        }
        public ActionResult EditSubject(int subjectId)
        {
            if (subjectId > 0)
            {
                var subject = this.subjectQueryService.GetSubject(subjectId);
                if (subject != null)
                {
                    var model = new SubjectEditor
                    {
                        Subject = subject.ToWeb(),
                        Tests = subject.Tests.Select(t => t.ToWeb()).ToList()
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
                    this.subjectCreationService.UpdateSubject(subject.ToBll());
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
                this.testCreationService.CreateTest(test.SubjectId, test.Name, test.Topic, test.Description);
                return RedirectToAction("EditSubject", "Admin", new { subjectId = test.SubjectId });
            }
            return View(test);
        }
        public ActionResult EditTest(int testId)
        {
            if (testId > 0)
            {
                var test = this.testQueryService.GetTest(testId);
                if (test != null)
                {
                    var model = new TestEditor
                    {
                        Test = test.ToWeb(),
                        Questions = test.Questions.Select(q => q.ToWeb()).ToList()
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
                    this.testCreationService.UpdateTest(test.ToBll());
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
                this.testQuestionsManagementService.AddNewTestQuestions(question.TestId, new List<BLL.Interface.Entities.Question> { question.ToBll() });
                return RedirectToAction("EditTest", "Admin", new { testId = question.TestId });
            }
            return View(question);
        }
        public ActionResult EditQuestion(int questionId)
        {
            if (questionId > 0)
            {
                var question = this.questionQueryService.GetQuestion(questionId);
                if (question != null)
                {
                    var model = new QuestionEditor
                    {
                        Question  = question.ToWeb(),
                        Answers = question.Answers.Select(a=> a.ToWeb()).ToList(),
                        Fakes = question.Fakes.Select(f => f.ToWeb()).ToList()
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
                    this.questionCreationService.UpdateQuestion(question.ToBll());
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
                this.questionAnswersManagmentService.AddNewQuestionAnswers(answer.QuestionId, new List<BLL.Interface.Entities.Answer> { answer.ToBll() });
                return RedirectToAction("EditQuestion", "Admin", new { questionId = answer.QuestionId });
            }
            return View(answer);
        }

        public ActionResult EditAnswer(int answerId)
        {
            if (answerId > 0)
            {
                var answer = this.answersQueryService.GetAnswer(answerId);
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
                    this.questionAnswersManagmentService.UpdateAnswer(answer.Id,answer.Text);
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
                this.questionAnswersManagmentService.AddNewQuestionFakes(fake.QuestionId, new List<BLL.Interface.Entities.Fake> { fake.ToBll() });
                return RedirectToAction("EditQuestion", "Admin", new { questionId = fake.QuestionId });

            }
            return View(fake);
        }

        public ActionResult EditFake(int fakeId)
        {
            if (fakeId > 0)
            {
                var fake = this.answersQueryService.GetFake(fakeId);
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
                    this.questionAnswersManagmentService.UpdateFake(fake.Id, fake.Text);
                }
                return RedirectToAction("EditFake", "Admin", new { fakeId = fake.Id });
            }
            return View(fake);
        }

        #endregion

    }
}

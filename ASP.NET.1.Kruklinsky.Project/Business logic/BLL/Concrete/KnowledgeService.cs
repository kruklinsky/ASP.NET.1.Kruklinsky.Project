using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Abstract;
using DAL.Interface.Abstract;
using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;

namespace BLL.Concrete
{
    public class KnowledgeService: IKnowledgeService
    {
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;
        private IDbContextScopeFactory dbContextScopeFactory;


        public KnowledgeService(IQuestionRepository questionRepository, ITestRepository testRepository, IDbContextScopeFactory dbContextScopeFactory)
        {
            if (questionRepository == null)
            {
                throw new System.ArgumentNullException("questionRepository", "Question repository is null.");
            }
            if (testRepository == null)
            {
                throw new System.ArgumentNullException("testRepository", "Test repository is null.");
            }
            if (dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.questionRepository = questionRepository;
            this.testRepository = testRepository;
            this.dbContextScopeFactory = dbContextScopeFactory;
        }


        #region Question

        #region QuestionQueryServise

        public Question GetQuestion(int id)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            Question result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var question = this.questionRepository.GetQuestion(id);
                if (question != null)
                {
                    result = question.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<Question> GetAllQuestions()
        {
            IEnumerable<Question> result = new List<Question>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var questions = this.questionRepository.Data;
                if (questions.Count() != 0)
                {
                    result = questions.Select(q => q.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion

        #region QuestionCreationServise

        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description)
        {
            SubjectExceptionsHelper.GetIdExceptions(subjectId);
            QuestionExceptionsHelper.GetLevelExceptions(level);
            QuestionExceptionsHelper.GetTopicExcetpions(topic);
            QuestionExceptionsHelper.GetTextExceptions(text);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.questionRepository.Add(newQuestion.ToDal());
                context.SaveChanges();
            }
        }
        public void CreateQuestion(int subjectId, int level, string topic, string text, string example, string description, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            SubjectExceptionsHelper.GetIdExceptions(subjectId);
            QuestionExceptionsHelper.GetLevelExceptions(level);
            QuestionExceptionsHelper.GetTopicExcetpions(topic);
            QuestionExceptionsHelper.GetTextExceptions(text);
            AnswerExceptionsHelper.GetAnswersExceptions(answers);
            AnswerExceptionsHelper.GetFakesExceptions(fakes);
            var newQuestion = new Question
            {
                SubjectId = subjectId,
                Level = level,
                Topic = topic,
                Text = text,
                Example = example,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.questionRepository.Add(newQuestion.ToDal(), answers.Select(a => a.ToDal()).ToList(), fakes.Select(f => f.ToDal()).ToList());
                context.SaveChanges();
            }
        }
        public bool DeleteQuestion(int id)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var question = this.GetQuestion(id);
                if (question != null)
                {
                    this.questionRepository.Delete(question.ToDal());
                    result = true;
                }
                context.SaveChanges();
            }
            return result;
        }
        public void UpdateQuestion(Question question)
        {
            QuestionExceptionsHelper.GetQuestionsEceptions(question);
            using (var context = dbContextScopeFactory.Create())
            {
                this.questionRepository.Update(question.ToDal());
                context.SaveChanges();
            }
        }

        #endregion

        #region QuestionAnswersManagmentServise

        public int AddNewQuestionAnswers(int id, IEnumerable<Answer> answers)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetAnswersExceptions(answers);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in answers)
                {
                    this.questionRepository.AddQuestionAnswer(id, item.ToDal());
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }
        public int DeleteQuestionAnswers(int id, IEnumerable<Answer> answers)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetAnswersExceptions(answers);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in answers)
                {
                    this.questionRepository.DeleteAnswer(item.Id);
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }

        public int AddNewQuestionFakes(int id, IEnumerable<Fake> fakes)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetFakesExceptions(fakes);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in fakes)
                {
                    this.questionRepository.AddQuestionFake(id, item.ToDal());
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }
        public int DeleteQuestionFakes(int id, IEnumerable<Fake> fakes)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetFakesExceptions(fakes);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in fakes)
                {
                    this.questionRepository.DeleteFake(item.Id);
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }

        public void UpdateAnswer(int id, string text)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetAnswerTextExceptions(text);
            using (var context = dbContextScopeFactory.Create())
            {
                this.questionRepository.UpdateAnswer(id, text);
                context.SaveChanges();
            }
        }
        public void UpdateFake(int id, string text)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetFakeTextExceptions(text);
            using (var context = dbContextScopeFactory.Create())
            {
                this.questionRepository.UpdateFake(id, text);
                context.SaveChanges();
            }
        }

        #endregion

        #region AnswersQueryServise

        public Answer GetAnswer(int id)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            Answer result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var answer = this.questionRepository.GetAnswer(id);
                if (answer != null)
                {
                    result = answer.ToBll();
                }
            }
            return result;
        }
        public Fake GetFake(int id)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            Fake result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var fake = this.questionRepository.GetFake(id);
                if (fake != null)
                {
                    result = fake.ToBll();
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region Test

        #region TestQueryService

        public Test GetTest(int id)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            Test result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var test = this.testRepository.GetTest(id);
                if (test != null)
                {
                    result = test.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<Test> GetAllTests()
        {
            IEnumerable<Test> result = new List<Test>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var tests = this.testRepository.Data;
                if (tests.Count() != 0)
                {
                    result = tests.Select(t => t.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion

        #region TestCreationService

        public void CreateTest(int subjectId, string name, string topic, string description)
        {
            SubjectExceptionsHelper.GetIdExceptions(subjectId);
            TestExceptionsHelper.GetNameExceptions(name);
            TestExceptionsHelper.GetTopicExceptions(topic);
            Test newTest = new Test
            {
                SubjectId = subjectId,
                Name = name,
                Topic = topic,
                Description = description
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.testRepository.Add(newTest.ToDal());
                context.SaveChanges();
            }
        }
        public bool DeleteTest(int id)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var test = this.testRepository.GetTest(id);
                if (test != null)
                {
                    this.testRepository.Delete(test);
                    result = true;
                }
                context.SaveChanges();
            }
            return result;
        }
        public void UpdateTest(Test test)
        {
            TestExceptionsHelper.GetIdExceptions(test.Id);
            TestExceptionsHelper.GetNameExceptions(test.Name);
            TestExceptionsHelper.GetTopicExceptions(test.Topic);
            using (var context = dbContextScopeFactory.Create())
            {
                this.testRepository.Update(test.ToDal());
                context.SaveChanges();
            }
        }

        #endregion

        #region TestQuestionsManagementService

        public int AddTestQuestions(int id, IEnumerable<int> questionsId)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            foreach (var item in questionsId)
            {
                QuestionExceptionsHelper.GetIdExceptions(item);
            }
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in questionsId)
                {
                    this.testRepository.AddTestQuestion(id, item);
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }
        public int AddNewTestQuestions(int id, IEnumerable<Question> questions)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            QuestionExceptionsHelper.GetQuestionsEceptions(questions);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in questions)
                {
                    this.testRepository.AddTestQuestion(id, item.ToDal(), item.Answers == null ? null : item.Answers.Select(a => a.ToDal()).ToList(), item.Fakes == null ? null : item.Fakes.Select(f => f.ToDal()).ToList());
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }
        public int DeleteTestQuestions(int id, IEnumerable<int> questionsId)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            foreach (var item in questionsId)
            {
                QuestionExceptionsHelper.GetIdExceptions(item);
            }
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in questionsId)
                {
                    this.testRepository.DeleteTestQuestion(id, item);
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }

        #endregion

        #endregion
    }
}

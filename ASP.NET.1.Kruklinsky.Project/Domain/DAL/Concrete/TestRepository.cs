using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;
using System.Data.Entity;
using AmbientDbContext.Interface;
using ORM;

namespace DAL.Concrete
{
    public class TestRepository: ITestRepository
    {
        #region IRepository

        private readonly IAmbientDbContextLocator ambientDbContextLocator;
        private DbContext context
        {
            get
            {
                var dbContext = this.ambientDbContextLocator.Get<EFDbContext>();
                if (dbContext == null)
                {
                    throw new InvalidOperationException("It is impossible to use repository because DbContextScope has not been created.");
                }
                return dbContext;
            }
        }

        public TestRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null)
            {
                throw new System.ArgumentNullException("ambientDbContextLocator", "Ambient dbContext locator is null.");
            }
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public IEnumerable<Test> Data
        {
            get
            {
                IEnumerable<ORM.Model.Test> result = this.context.Set<ORM.Model.Test>();
                return result.Select(t => t.ToDal()).ToList();
            }
        }
        public void Add(Test item)
        {
            this.context.Set<ORM.Model.Test>().Add(item.ToOrm());
            this.context.SaveChanges();
        }
        public void Delete(Test item)
        {
            var result = this.GetOrmTest(item.Id);
            if (result != null)
            {
                this.context.Set<ORM.Model.Test>().Remove(result);
                this.context.SaveChanges();
            }
        }
        public void Update(Test item)
        {
            var result = this.GetOrmTest(item.Id);
            if (result != null)
            {
                result.Name = item.Name;
                result.Topic = item.Topic;
                result.Description = item.Description;
                this.context.SaveChanges();
            }
        }

        #endregion

        #region ITestRepository

        public Test GetTest(int id)
        {
            Test result = null;
            var test = this.GetOrmTest(id);
            if(test != null)
            {
                result = test.ToDal();
            }
            return result;
        }

        public IEnumerable<Question> GetTestQuestions(int id)
        {
            IEnumerable<Question> result = new List<Question>();
            var test = this.GetOrmTest(id);
            if (test != null && test.Questions != null)
            {
                result = test.Questions.Select(q => q.ToDal()).ToList();
            }
            return result;
        }
        public void AddTestQuestion(int id, int questionId)
        {
            var test = this.GetOrmTest(id);
            var question = this.GetOrmQuestion(questionId);
            if (test != null && question != null)
            {
                if (test.Questions == null) test.Questions = new List<ORM.Model.Question>();
                test.Questions.Add(question);
                this.context.SaveChanges();
            }
        }
        public void AddTestQuestion(int id, Question question, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            var test = this.GetOrmTest(id);
            if (test != null)
            {
                if (test.Questions == null) test.Questions = new List<ORM.Model.Question>();
                test.Questions.Add(question.ToOrm(answers, fakes));
                this.context.SaveChanges();
            }
        }
        public void DeleteTestQuestion(int id, int questionId)
        {
            var test = this.GetOrmTest(id);
            var query = test.Questions.Where(q => q.QuestionId == questionId);
            if (test != null && query.Count() != 0)
            {
                test.Questions.Remove(query.First());
                this.context.SaveChanges();
            }
        }

        #endregion

        #region Private methods

        private ORM.Model.Test GetOrmTest(int testId)
        {
            ORM.Model.Test result = null;
            var query = this.context.Set<ORM.Model.Test>().Where(q => q.TestId == testId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }
        private ORM.Model.Question GetOrmQuestion(int questionId)
        {
            ORM.Model.Question result = null;
            var query = this.context.Set<ORM.Model.Question>().Where(q => q.QuestionId == questionId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion
    }
}

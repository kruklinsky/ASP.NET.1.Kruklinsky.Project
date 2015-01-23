using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using AmbientDbContext.Interface;
using ORM;

namespace DAL.Concrete
{
    public class QuestionRepository : IQuestionRepository
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

        public QuestionRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if (ambientDbContextLocator == null)
            {
                throw new System.ArgumentNullException("ambientDbContextLocator", "Ambient dbContext locator is null.");
            }
            this.ambientDbContextLocator = ambientDbContextLocator;
        }

        public IEnumerable<Question> Data
        {
            get 
            {
                IEnumerable<ORM.Model.Question> result = this.context.Set<ORM.Model.Question>();
                return result.Select(q => q.ToDal()).ToList();
            }
        }
        public void Add(Question item)
        {
            this.context.Set<ORM.Model.Question>().Add(item.ToOrm());
            this.context.SaveChanges();
        }
        public void Delete(Question item)
        {
            var result = this.GetOrmQuestion(item.Id);
            if (result != null && ( result.Tests == null || result.Tests.Count == 0))
            {
                this.context.Set<ORM.Model.Question>().Remove(result);
                this.context.SaveChanges();
            }
        }
        public void Update(Question item)
        {
            var result = this.GetOrmQuestion(item.Id);
            if (result != null)
            {
                result.Level = item.Level;
                result.Topic = item.Topic;
                result.Text = item.Text;
                result.Example = item.Example;
                result.Description = item.Description;
                this.context.SaveChanges();
            }
        }

        #endregion

        #region IQuestionRepository

        public void Add(Question item, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            this.context.Set<ORM.Model.Question>().Add(item.ToOrm(answers, fakes));
            this.context.SaveChanges();
        }
        public Question GetQuestion(int id)
        {
            Question result = null;
            var question = this.GetOrmQuestion(id);
            if (question != null)
            {
                result = question.ToDal();
            }
            return result;
        }

        public IEnumerable<Answer> GetQuestionAnswers(int id)
        {
            IEnumerable<Answer> result = new List<Answer>();
            var question = this.GetOrmQuestion(id);
            if (question != null && question.Answers != null)
            {
                result = question.Answers.Select(a => a.ToDal()).ToList();
            }
            return result;
        }
        public void AddQuestionAnswer(int id, Answer answer)
        {
            var question = this.GetOrmQuestion(id);
            if (question != null)
            {
                if (question.Answers == null) question.Answers = new List<ORM.Model.Answer>();
                question.Answers.Add(answer.ToOrm());
                this.context.SaveChanges();
            }
        }
        public void DeleteAnswer(int id)
        {
            var answer = this.GetOrmAnswer(id);
            if (answer != null)
            {
                this.context.Set<ORM.Model.Answer>().Remove(answer);
                this.context.SaveChanges();
            }
        }
        public void UpdateAnswer(int id, string text)
        {
            var answer = this.GetOrmAnswer(id);
            if (answer != null)
            {
                answer.Text = text;
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Fake> GetQuestionFakes(int id)
        {
            IEnumerable<Fake> result = new List<Fake>();
            var question = this.GetOrmQuestion(id);
            if (question != null && question.Fakes != null)
            {
                result = question.Fakes.Select(a => a.ToDal()).ToList();
            }
            return result;
        }
        public void AddQuestionFake(int id, Fake fake)
        {
            var question = this.GetOrmQuestion(id);
            if (question != null)
            {
                if (question.Fakes == null) question.Fakes = new List<ORM.Model.Fake>();
                question.Fakes.Add(fake.ToOrm());
                this.context.SaveChanges();
            }
        }
        public void DeleteFake(int id)
        {
            var fake = this.GetOrmFake(id);
            if (fake != null)
            {
                this.context.Set<ORM.Model.Fake>().Remove(fake);
                this.context.SaveChanges();
            }
        }
        public void UpdateFake(int id, string text)
        {
            var fake = this.GetOrmFake(id);
            if (fake != null)
            {
                fake.Text = text;
                this.context.SaveChanges();
            }
        }

        public Answer GetAnswer(int id)
        {
            Answer result = null;
            var answer = this.GetOrmAnswer(id);
            if(answer != null)
            {
                result = answer.ToDal();
            }
            return result;
        }

        public Fake GetFake(int id)
        {
            Fake result = null;
            var fake = this.GetOrmFake(id);
            if (fake != null)
            {
                result = fake.ToDal();
            }
            return result;
        }

        #endregion

        #region Private methods

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

        private ORM.Model.Answer GetOrmAnswer(int answerId)
        {
            ORM.Model.Answer result = null;
            var query = this.context.Set<ORM.Model.Answer>().Where(a => a.AnswerId == answerId);
            if(query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.Fake GetOrmFake(int fakeId)
        {
            ORM.Model.Fake result = null;
            var query = this.context.Set<ORM.Model.Fake>().Where(a => a.FakeId == fakeId);
            if (query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        #endregion
    }
}

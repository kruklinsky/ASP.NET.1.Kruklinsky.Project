﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Abstract;
using DAL.Interface.Entities;
using System.Data.Entity;

namespace DAL.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        #region IRepository

        private readonly DbContext context;
        public QuestionRepository(DbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Question> Data
        {
            get 
            {
                IEnumerable<ORM.Model.Question> result = this.context.Set<ORM.Model.Question>();
                return result.Select(q => q.ToDal());
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
        public Question GetQuestion(int Id)
        {
            Question result = null;
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                result = question.ToDal();
            }
            return result;
        }
        public Question GetQuestion(int Id, out IEnumerable<Answer> answers, out IEnumerable<Fake> fakes)
        {
            Question result = null;
            answers = new List<Answer>();
            fakes = new List<Fake>();
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                result = question.ToDal();
                answers = question.Answers.Select(a => a.ToDal());
                fakes = question.Fakes.Select(f => f.ToDal());
            }
            return result;
        }

        public IEnumerable<Answer> GetQuestionAnswers(int Id)
        {
            IEnumerable<Answer> result = new List<Answer>();
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                result = question.Answers.Select(a => a.ToDal());
            }
            return result;
        }
        public void AddQuestionAnswer(int Id, Answer answer)
        {
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                question.Answers.Add(answer.ToOrm());
                this.context.SaveChanges();
            }
        }
        public void DeleteAnswer(int Id)
        {
            var answer = this.GetAnswer(Id);
            if (answer != null)
            {
                this.context.Set<ORM.Model.Answer>().Remove(answer);
                this.context.SaveChanges();
            }
        }
        public void UpdateAnswer(int Id, string text)
        {
            var answer = this.GetAnswer(Id);
            if (answer != null)
            {
                answer.Text = text;
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Fake> GetQuestionFakes(int Id)
        {
            IEnumerable<Fake> result = new List<Fake>();
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                result = question.Fakes.Select(a => a.ToDal());
            }
            return result;
        }
        public void AddQuestionFake(int Id, Fake fake)
        {
            var question = this.GetOrmQuestion(Id);
            if (question != null)
            {
                question.Fakes.Add(fake.ToOrm());
                this.context.SaveChanges();
            }
        }
        public void DeleteFake(int Id)
        {
            var fake = this.GetFake(Id);
            if (fake != null)
            {
                this.context.Set<ORM.Model.Fake>().Remove(fake);
                this.context.SaveChanges();
            }
        }
        public void UpdateFake(int Id, string text)
        {
            var fake = this.GetFake(Id);
            if (fake != null)
            {
                fake.Text = text;
                this.context.SaveChanges();
            }
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

        private ORM.Model.Answer GetAnswer(int answerId)
        {
            ORM.Model.Answer result = null;
            var query = this.context.Set<ORM.Model.Answer>().Where(a => a.AnswerId == answerId);
            if(query.Count() != 0)
            {
                result = query.First();
            }
            return result;
        }

        private ORM.Model.Fake GetFake(int fakeId)
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
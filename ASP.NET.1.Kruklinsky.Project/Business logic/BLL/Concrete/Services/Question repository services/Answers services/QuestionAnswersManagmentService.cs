using AmbientDbContext.Interface;
using BLL.Concrete.ExceptionsHelpers;
using BLL.Interface.Abstract;
using BLL.Interface.Entities;
using DAL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class QuestionAnswersManagmentServise : RepositoryService<IQuestionRepository>, IQuestionAnswersManagmentService
    {
        public QuestionAnswersManagmentServise(IQuestionRepository questionRepository, IDbContextScopeFactory dbContextScopeFactory) : base(questionRepository, dbContextScopeFactory) { }

        #region IQuestionAnswersManagmentServise

        public int AddNewQuestionAnswers(int id, IEnumerable<Answer> answers)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetAnswersExceptions(answers);
            int result = 0;
            using (var context = dbContextScopeFactory.Create())
            {
                foreach (var item in answers)
                {
                    this.repository.AddQuestionAnswer(id, item.ToDal());
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
                    this.repository.DeleteAnswer(item.Id);
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
                    this.repository.AddQuestionFake(id, item.ToDal());
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
                    this.repository.DeleteFake(item.Id);
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
                this.repository.UpdateAnswer(id, text);
                context.SaveChanges();
            }
        }
        public void UpdateFake(int id, string text)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            AnswerExceptionsHelper.GetFakeTextExceptions(text);
            using (var context = dbContextScopeFactory.Create())
            {
                this.repository.UpdateFake(id, text);
                context.SaveChanges();
            }
        }

        #endregion
    }
}

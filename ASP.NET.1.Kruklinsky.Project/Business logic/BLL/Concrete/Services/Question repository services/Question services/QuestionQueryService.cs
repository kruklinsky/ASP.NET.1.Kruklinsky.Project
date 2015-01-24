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
    public class QuestionQueryService : RepositoryService<IQuestionRepository>, IQuestionQueryService
    {
        public QuestionQueryService(IQuestionRepository questionRepository, IDbContextScopeFactory dbContextScopeFactory) : base(questionRepository,dbContextScopeFactory) {}

        #region IQuestionQueryServise

        public Question GetQuestion(int id)
        {
            QuestionExceptionsHelper.GetIdExceptions(id);
            Question result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var question = this.repository.GetQuestion(id);
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
                var questions = this.repository.Data;
                if (questions.Count() != 0)
                {
                    result = questions.Select(q => q.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion
    }
}

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
    public class AnswersQueryServise : RepositoryService<IQuestionRepository>, IAnswersQueryService
    {
        public AnswersQueryServise(IQuestionRepository questionRepository, IDbContextScopeFactory dbContextScopeFactory) : base(questionRepository,dbContextScopeFactory) {}

        #region IAnswersQueryServise

        public Answer GetAnswer(int id)
        {
            AnswerExceptionsHelper.GetIdExceptions(id);
            Answer result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var answer = this.repository.GetAnswer(id);
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
                var fake = this.repository.GetFake(id);
                if (fake != null)
                {
                    result = fake.ToBll();
                }
            }
            return result;
        }

        #endregion
    }
}

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
    public class TestQuestionsManagementService : RepositoryService<ITestRepository>, ITestQuestionsManagementService
    {
        public TestQuestionsManagementService(ITestRepository testRepository, IDbContextScopeFactory dbContextScopeFactory) : base(testRepository,dbContextScopeFactory) {}

        #region ITestQuestionsManagementService

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
                    this.repository.AddTestQuestion(id, item);
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
                    this.repository.AddTestQuestion(id, item.ToDal(), item.Answers == null ? null : item.Answers.Select(a => a.ToDal()).ToList(), item.Fakes == null ? null : item.Fakes.Select(f => f.ToDal()).ToList());
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
                    this.repository.DeleteTestQuestion(id, item);
                    result++;
                }
                context.SaveChanges();
            }
            return result;
        }

        #endregion
    }
}

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
    public class TestCreationService : RepositoryService<ITestRepository>, ITestCreationService
    {
        public TestCreationService(ITestRepository testRepository, IDbContextScopeFactory dbContextScopeFactory) : base(testRepository,dbContextScopeFactory) {}

        #region ITestCreationService

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
                this.repository.Add(newTest.ToDal());
                context.SaveChanges();
            }
        }
        public bool DeleteTest(int id)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            bool result = false;
            using (var context = dbContextScopeFactory.Create())
            {
                var test = this.repository.GetTest(id);
                if (test != null)
                {
                    this.repository.Delete(test);
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
                this.repository.Update(test.ToDal());
                context.SaveChanges();
            }
        }

        #endregion
    }
}

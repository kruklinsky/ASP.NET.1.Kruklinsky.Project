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
    public class TestQueryService : RepositoryService<ITestRepository>, ITestQueryService
    {
        public TestQueryService(ITestRepository testRepository, IDbContextScopeFactory dbContextScopeFactory) : base(testRepository,dbContextScopeFactory) {}

        #region ITestQueryService

        public Test GetTest(int id)
        {
            TestExceptionsHelper.GetIdExceptions(id);
            Test result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var test = this.repository.GetTest(id);
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
                var tests = this.repository.Data;
                if (tests.Count() != 0)
                {
                    result = tests.Select(t => t.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion
    }
}

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
    public class TestService : ITestService
    {
        private IResultRepository resultRepository;
        private IDbContextScopeFactory dbContextScopeFactory;

        public TestService(IResultRepository resultRepository, IDbContextScopeFactory dbContextScopeFactory)
        {
            if (resultRepository == null)
            {
                throw new System.ArgumentNullException("resultRepository", "Result repository is null.");
            }
            if (dbContextScopeFactory == null)
            {
                throw new System.ArgumentNullException("dbContextScopeFactory", "DbContextScope factory is null.");
            }
            this.resultRepository = resultRepository;
            this.dbContextScopeFactory = dbContextScopeFactory;
        }

        #region ITestService

        public int StartTest(string userId, int testId, int duration)
        {
            UserExceptionsHelper.GetIdExceptions(userId);
            TestExceptionsHelper.GetIdExceptions(testId);
            TestExceptionsHelper.GetDurationExceptions(duration);
            int result = -1;
            this.CreateResult(userId, testId, DateTime.UtcNow, new TimeSpan(duration / 60, duration % 60, 0));
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var lastResult = this.resultRepository.GetLastResult();
                if (lastResult != null)
                {
                    result = lastResult.Id;
                }
            }
            return result;
        }
        public TimeSpan TimeLeft(int resultId)
        {
            ResultExceptionsHelper.GetIdExceptions(resultId);
            TimeSpan timeLeft = new TimeSpan();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var result = this.resultRepository.GetResult(resultId);
                if (result != null && (result.Answers == null || result.Answers.Value.Count() == 0))
                {
                    timeLeft = result.Time - (DateTime.UtcNow - result.Start);
                }
            }
            return timeLeft;
        }

        public void FinishTest(int resultId, IEnumerable<Interface.Entities.UserAnswer> answers)
        {
            ResultExceptionsHelper.GetIdExceptions(resultId);
            using (var context = dbContextScopeFactory.Create())
            {
                var result = this.resultRepository.GetResult(resultId);
                if (result != null && (result.Answers == null || result.Answers.Value.Count() == 0))
                {
                    result.Time = (DateTime.UtcNow - result.Start);
                    result.Answers = new Lazy<IEnumerable<DAL.Interface.Entities.UserAnswer>>(() => answers.Select(a => a.ToDal()).ToList());
                    this.resultRepository.Update(result);
                }
                context.SaveChanges();
            }
        }

        private void CreateResult(string userId, int testId, DateTime start, TimeSpan time)
        {
            UserExceptionsHelper.GetIdExceptions(userId);
            TestExceptionsHelper.GetIdExceptions(testId);
            Result newResult = new Result
            {
                UserId = userId,
                TestId = testId,
                Start = start,
                Time = time
            };
            using (var context = dbContextScopeFactory.Create())
            {
                this.resultRepository.Add(newResult.ToDal());
                context.SaveChanges();
            }
        }

        #endregion

        #region ResultsService

        public Result GetResult(int id)
        {
            ResultExceptionsHelper.GetIdExceptions(id);
            Result result = null;
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var dalResult = this.resultRepository.GetResult(id);
                if (dalResult != null)
                {
                    result = dalResult.ToBll();
                }
            }
            return result;
        }
        public IEnumerable<Result> GetAllResults()
        {
            IEnumerable<Result> result = new List<Result>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var results = this.resultRepository.Data;
                if (results.Count() != 0)
                {
                    result = results.Select(r => r.ToBll()).ToList();
                }
            }
            return result;
        }
        public IEnumerable<Result> GetUserResults(string userId)
        {
            UserExceptionsHelper.GetIdExceptions(userId);
            IEnumerable<Result> result = new List<Result>();
            using (var context = dbContextScopeFactory.CreateReadOnly())
            {
                var results = this.resultRepository.GetUserResults(userId);
                if (results.Count() != 0)
                {
                    result = results.Select(r => r.ToBll()).ToList();
                }
            }
            return result;
        }

        #endregion
    }
}

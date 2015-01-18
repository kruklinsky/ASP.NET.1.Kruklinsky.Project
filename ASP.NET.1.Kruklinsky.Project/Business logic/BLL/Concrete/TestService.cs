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
        IResultRepository resultRepository;

        public TestService(IResultRepository resultRepository)
        {
            if (resultRepository == null)
            {
                throw new System.ArgumentNullException("resultRepository", "Result repository is null.");
            }
            this.resultRepository = resultRepository;
        }

        #region ITestService

        public Result GetResult(int id)
        {
            this.GetIdExceptions(id);
            Result result = null;
            var dalResult = this.resultRepository.GetResult(id);
            if(dalResult != null)
            {
                result = dalResult.ToBll();
            }
            return result;
        }
        public IEnumerable<Result> GetAllResults ()
        {
            IEnumerable<Result> result = new List<Result>();
            var results = this.resultRepository.Data;
            if (results.Count() != 0)
            {
                result = results.Select(r => r.ToBll()).ToList();
            }
            return result;
        }
        public IEnumerable<Result> GetUserResults(string userId)
        {
            this.GetIdExceptions(userId);
            IEnumerable<Result> result = new List<Result>();
            var results = this.resultRepository.GetUserResults(userId);
            if(results.Count() != 0)
            {
                result = results.Select(r => r.ToBll()).ToList();
            }
            return result;
        }

        public int StartTest(string userId, int testId, int duration)
        {
            this.GetIdExceptions(userId);
            this.GetIdExceptions(testId);
            this.GetDurationExceptions(duration);
            int result = -1;
            this.CreateResult(userId, testId, DateTime.UtcNow, new TimeSpan(duration / 60, duration % 60, 0));
            var lastResult = this.resultRepository.GetLastResult();
            if (lastResult != null)
            {
                result = lastResult.Id;
            }
            return result;
        }
        public TimeSpan TimeLeft(int resultId)
        {
            this.GetIdExceptions(resultId);
            TimeSpan timeLeft = new TimeSpan();
            Result result = this.GetResult(resultId);
            if (result != null && (result.Answers.Value == null || result.Answers.Value.Count() == 0))
            {
                timeLeft = result.Time - (DateTime.UtcNow - result.Start);
            }
            return timeLeft;
        }
        public void FinishTest(int resultId, IEnumerable<Interface.Entities.UserAnswer> answers)
        {
            this.GetIdExceptions(resultId);
            Result result = this.GetResult(resultId);
            if (result != null && (result.Answers.Value == null || result.Answers.Value.Count() == 0))
            {
                result.Time = (DateTime.UtcNow - result.Start);
                this.resultRepository.Update(result.ToDal(answers));
            }
        }

        private void CreateResult(string userId, int testId, DateTime start, TimeSpan time)
        {
            this.GetIdExceptions(userId);
            this.GetIdExceptions(testId);
            Result newResult = new Result
            {
                UserId = userId,
                TestId = testId,
                Start = start,
                Time = time
            };
            this.resultRepository.Add(newResult.ToDal());
        }

        #endregion

        #region Private methods

        private void GetIdExceptions(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentOutOfRangeException("id", id, "Id is less than zero.");
            }
        }
        private void GetIdExceptions(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new System.ArgumentException("User id is null, empty or consists only of white-space characters.", "id");
            }
            if (!IsGuid(id))
            {
                throw new System.ArgumentException("Cannot convert user id to guid.", "id");
            }
        }
        private bool IsGuid(string id)
        {
            Guid temp;
            return Guid.TryParse(id, out temp);
        }

        private void GetDurationExceptions(int duration)
        {
            if (duration < 0)
            {
                throw new System.ArgumentOutOfRangeException("duration", duration, "Duration is less than zero.");
            }
        }

        #endregion

    }
}

using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface ITestService
    {

        Result GetResult(int id);
        IEnumerable<Result> GetUserResults(string userId);

        int StartTest(string userId, int testId, int duration);
        TimeSpan TimeLeft(int resultId);
        void FinishTest(int resultId, IEnumerable<UserAnswer> answers);
    }
}

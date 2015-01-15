using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface IResultRepository: IRepository<Result>
    {
        Result GetResult(int id);
        Result GetLastResult();

        IEnumerable<UserAnswer> GetUserAnswers(int resultId);
        void AddUserAnswer(int resultId, UserAnswer answer);

        IEnumerable<Result> GetUserResults(string userId);
    }
}

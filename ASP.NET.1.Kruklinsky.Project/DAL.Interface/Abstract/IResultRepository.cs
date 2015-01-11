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
        Result GetResult(int Id);
        Result GetResult(int Id, out IEnumerable<UserAnswer> answers);

        IEnumerable<UserAnswer> GetResultAnswers(int Id);
        void AddResultAnswer(int Id, UserAnswer answer);
    }
}

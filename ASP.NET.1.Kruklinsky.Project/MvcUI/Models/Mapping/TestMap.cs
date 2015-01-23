using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Models
{
    public static class TestMap
    {
        public static Result ToWeb(this BLL.Interface.Entities.Result item)
        {
            return new Result
            {
                Id = item.Id,
                TestId = item.TestId,
                UserId = item.UserId,
                Start = item.Start,
                Time = item.Time,
                Answers = new Lazy<IEnumerable<UserAnswer>>(() => item.Answers == null ? new List<UserAnswer>() : item.Answers.Select(a => a.ToWeb()).ToList())
            };
        }

        public static UserAnswer ToWeb(this BLL.Interface.Entities.UserAnswer item)
        {
            return new UserAnswer
            {
                Id = item.Id,
                QuestionId = item.QuestionId,
                ResultId = item.ResultId,
                IsRight = item.IsRight
            };
        }
    }
}

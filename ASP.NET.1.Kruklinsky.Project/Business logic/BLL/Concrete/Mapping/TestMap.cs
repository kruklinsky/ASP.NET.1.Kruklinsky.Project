using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class TestMap
    {
        public static DAL.Interface.Entities.Result ToDal(this Result item)
        {
            return new DAL.Interface.Entities.Result
            {
                Id = item.Id,
                TestId = item.TestId,
                UserId = item.UserId,
                Start = item.Start,
                Time = item.Time
            };
        }
        public static DAL.Interface.Entities.Result ToDal(this Result item, IEnumerable<UserAnswer> answers)
        {
            return new DAL.Interface.Entities.Result
            {
                Id = item.Id,
                TestId = item.TestId,
                UserId = item.UserId,
                Start = item.Start,
                Time = item.Time,
                Answers = new Lazy<IEnumerable<DAL.Interface.Entities.UserAnswer>>(() => answers.Select(a => a.ToDal()).ToList())
            };
        }
        public static Result ToBll(this DAL.Interface.Entities.Result item)
        {
            return new Result
            {
                Id = item.Id,
                TestId = item.TestId,
                UserId = item.UserId,
                Start = item.Start,
                Time = item.Time,
                Answers = item.Answers == null ? new List<UserAnswer>() : item.Answers.Value.Select(a => a.ToBll()).ToList()
            };
        }

        public static UserAnswer ToBll(this DAL.Interface.Entities.UserAnswer item)
        {
            return new UserAnswer
            {
                Id = item.Id,
                QuestionId = item.QuestionId,
                ResultId = item.ResultId,
                IsRight = item.IsRight
            };
        }
        public static DAL.Interface.Entities.UserAnswer ToDal(this UserAnswer item)
        {
            return new DAL.Interface.Entities.UserAnswer
            {
                Id = item.Id,
                QuestionId = item.QuestionId,
                ResultId = item.ResultId,
                IsRight = item.IsRight
            };
        }
    }
}

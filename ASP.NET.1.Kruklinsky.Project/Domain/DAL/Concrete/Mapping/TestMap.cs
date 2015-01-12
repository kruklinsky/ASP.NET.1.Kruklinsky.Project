using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public static class TestMap
    {
        public static ORM.Model.Test ToOrm(this Test item)
        {
            return new ORM.Model.Test
            {
                TestId = item.Id,
                SubjectId = item.SubjectId,
                Name = item.Name,
                Topic = item.Topic,
                Description = item.Description
            };
        }
        public static ORM.Model.Test ToOrm(this Test item, IEnumerable<Question> questions)
        {
            return new ORM.Model.Test
            {
                TestId = item.Id,
                SubjectId = item.SubjectId,
                Name = item.Name,
                Topic = item.Topic,
                Description = item.Description,
                Questions = questions.Select(q => q.ToOrm()).ToList()
            };
        }
        public static Test ToDal(this ORM.Model.Test item)
        {
            return new Test
            {
                Id = item.TestId,
                SubjectId = item.SubjectId,
                Name = item.Name,
                Topic = item.Topic,
                Description = item.Description
            };
        }

        public static ORM.Model.Result ToOrm(this Result item)
        {
            return new ORM.Model.Result
            {
                ResultId = item.Id,
                TestId = item.TestId,
                UserId = item.UserId.ToGuid(),
                Time = item.Time
            };
        }
        public static ORM.Model.Result ToOrm(this Result item, IEnumerable<UserAnswer> answers)
        {
            return new ORM.Model.Result
            {
                ResultId = item.Id,
                TestId = item.TestId,
                UserId = item.UserId.ToGuid(),
                Time = item.Time,
                Answers = answers.Select(a => a.ToOrm()).ToList()
            };
        }
        public static Result ToDal(this ORM.Model.Result item)
        {
            return new Result
            {
                Id = item.ResultId,
                TestId = item.ResultId,
                UserId = item.UserId.ToString(),
                Time = item.Time
            };
        }

        public static ORM.Model.UserAnswer ToOrm(this UserAnswer item)
        {
            return new ORM.Model.UserAnswer
            {
                 AnswerId = item.Id,
                 QuestionId = item.QuestionId,
                 ResultId = item.ResultId,
                 IsRight = item.IsRight
            };
        }
        public static UserAnswer ToDal (this ORM.Model.UserAnswer item)
        {
            return new UserAnswer
            {
                Id = item.AnswerId,
                QuestionId = item.QuestionId,
                ResultId = item.ResultId,
                IsRight = item.IsRight
            };
        }

    }
}

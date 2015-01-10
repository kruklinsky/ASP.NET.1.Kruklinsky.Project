using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public static class KnowledgeMap
    {
        public static ORM.Model.Question ToOrm (this Question item)
        {
            return new ORM.Model.Question
            {
                 QuestionId = item.Id,
                 SubjectId = item.SubjectId,
                 Level = item.Level,
                 Topic = item.Topic,
                 Text = item.Text,
                 Description = item.Description,
                 Answers = new List<ORM.Model.Answer>(),
                 Fakes = new List<ORM.Model.Fake>()
            };
        }
        public static ORM.Model.Question ToOrm(this Question item, IEnumerable<Answer> answers, IEnumerable<Fake> fakes)
        {
            return new ORM.Model.Question
            {
                QuestionId = item.Id,
                SubjectId = item.SubjectId,
                Level = item.Level,
                Topic = item.Topic,
                Text = item.Text,
                Description = item.Description,
                Answers = answers.Select(a => a.ToOrm()).ToList(),
                Fakes = fakes.Select(f => f.ToOrm()).ToList()
            };
        }
        public static Question ToDal (this ORM.Model.Question item)
        {
            return new Question
            {
                 Id = item.QuestionId,
                 SubjectId = item.SubjectId,
                 Level = item.Level,
                 Topic = item.Topic,
                 Text = item.Text,
                 Description = item.Description
            };
        }

        public static ORM.Model.Answer ToOrm (this Answer item)
        {
            return new ORM.Model.Answer
            {
                 AnswerId = item.Id,
                 Text = item.Text
            };
        }
        public static Answer ToDal (this ORM.Model.Answer item)
        {
            return new Answer
            {
                Id = item.AnswerId,
                Text = item.Text
            };
        }

        public static ORM.Model.Fake ToOrm(this Fake item)
        {
            return new ORM.Model.Fake
            {
                FakeId = item.Id,
                Text = item.Text
            };
        }
        public static Fake ToDal(this ORM.Model.Fake item)
        {
            return new Fake
            {
                Id = item.FakeId,
                Text = item.Text
            };
        }

        public static ORM.Model.Test ToOrm (this Test item)
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
        public static Test ToDal (this ORM.Model.Test item)
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

    }
}

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

        public static ORM.Model.Subject ToOrm (this Subject item)
        {
            return new ORM.Model.Subject
            {
                 SubjectId = item.Id,
                 Name = item.Name,
                 Description = item.Description
            };
        }
        public static Subject ToDal (this ORM.Model.Subject item)
        {
            return new Subject
            {
                Id = item.SubjectId,
                Name = item.Name,
                Description = item.Description
            };
        }
    }
}

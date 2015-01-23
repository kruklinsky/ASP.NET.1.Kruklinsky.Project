using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class KnowledgeMap
    {
        public static Subject ToBll (this DAL.Interface.Entities.Subject item)
        {
            return new Subject
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Tests = item.Tests == null ? new List<Test>() : item.Tests.Value.Select(t => t.ToBll()).ToList()
            };
        }
        public static DAL.Interface.Entities.Subject ToDal(this Subject item)
        {
            return new DAL.Interface.Entities.Subject
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }

        public static Question ToBll(this DAL.Interface.Entities.Question item)
        {
            return new Question
            {
                Id = item.Id,
                SubjectId = item.SubjectId,
                Topic = item.Topic,
                Level = item.Level,
                Text = item.Text,
                Example = item.Example,
                Description = item.Description,
                Answers = item.Answers == null ? new List<Answer>() : item.Answers.Value.Select(a => a.ToBll()).ToList(),
                Fakes = item.Fakes == null ? new List<Fake>() : item.Fakes.Value.Select(f => f.ToBll()).ToList()
            };
        }
        public static DAL.Interface.Entities.Question ToDal(this Question item)
        {
            return new DAL.Interface.Entities.Question
            {
                Id = item.Id,
                SubjectId = item.SubjectId,
                Topic = item.Topic,
                Level = item.Level,
                Text = item.Text,
                Example = item.Example,
                Description = item.Description
            };
        }

        public static Answer ToBll(this DAL.Interface.Entities.Answer item)
        {
            return new Answer
            {
                Id = item.Id,
                Text = item.Text
            };
        }
        public static DAL.Interface.Entities.Answer ToDal(this Answer item)
        {
            return new DAL.Interface.Entities.Answer
            {
                Id = item.Id,
                Text = item.Text
            };
        }
        
        public static Fake ToBll(this DAL.Interface.Entities.Fake item)
        {
            return new Fake
            {
                Id = item.Id,
                Text = item.Text
            };
        }
        public static DAL.Interface.Entities.Fake ToDal(this Fake item)
        {
            return new DAL.Interface.Entities.Fake
            {
                Id = item.Id,
                Text = item.Text
            };
        }

        public static Test ToBll(this DAL.Interface.Entities.Test item)
        {
            return new Test
            {
                Id = item.Id,
                SubjectId = item.SubjectId,
                Topic = item.Topic,
                Name = item.Name,
                Description = item.Description,
                Questions = item.Questions == null ? new List<Question>() : item.Questions.Value.Select(q => q.ToBll()).ToList()
            };
        }

        public static DAL.Interface.Entities.Test ToDal(this Test item)
        {
            return new DAL.Interface.Entities.Test
            {
                Id = item.Id,
                SubjectId = item.SubjectId,
                Topic = item.Topic,
                Name = item.Name,
                Description = item.Description,
            };
        }

    }
}

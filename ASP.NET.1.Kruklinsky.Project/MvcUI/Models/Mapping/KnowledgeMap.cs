using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Models
{
    public static class KnowledgeMap
    {
        public static Subject ToWeb(this BLL.Interface.Entities.Subject item)
        {
            return new Subject
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
            };
        }

        public static BLL.Interface.Entities.Subject ToBll(this Subject item)
        {
            return new BLL.Interface.Entities.Subject
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };
        }

        public static Question ToWeb(this BLL.Interface.Entities.Question item)
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
            };
        }
        public static BLL.Interface.Entities.Question ToBll(this Question item)
        {
            return new BLL.Interface.Entities.Question
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

        public static Answer ToWeb(this BLL.Interface.Entities.Answer item)
        {
            return new Answer
            {
                Id = item.Id,
                Text = item.Text
            };
        }
        public static BLL.Interface.Entities.Answer ToBll(this Answer item)
        {
            return new BLL.Interface.Entities.Answer
            {
                Id = item.Id,
                Text = item.Text
            };
        }

        public static Fake ToWeb(this BLL.Interface.Entities.Fake item)
        {
            return new Fake
            {
                Id = item.Id,
                Text = item.Text
            };
        }
        public static BLL.Interface.Entities.Fake ToBll(this Fake item)
        {
            return new BLL.Interface.Entities.Fake
            {
                Id = item.Id,
                Text = item.Text
            };
        }

        public static Test ToWeb(this BLL.Interface.Entities.Test item)
        {
            return new Test
            {
                Id = item.Id,
                SubjectId = item.SubjectId,
                Topic = item.Topic,
                Name = item.Name,
                Description = item.Description
            };
        }
        public static BLL.Interface.Entities.Test ToBll(this Test item)
        {
            return new BLL.Interface.Entities.Test
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

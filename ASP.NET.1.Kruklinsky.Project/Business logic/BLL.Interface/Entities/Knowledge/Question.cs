using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int Level { get; set; }

        public string Topic { get; set; }

        public string Text { get; set; }

        public string Example { get; set; }

        public string Description { get; set; }


        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Fake> Fakes { get; set; }
    }
}

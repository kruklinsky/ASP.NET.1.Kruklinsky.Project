using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }

        public int SubjectId { get; set; }

        public int Level { get; set; }

        public string Topic { get; set; }

        public string Text { get; set; }

        public string Example { get; set; }

        public string Description { get; set; }


        public virtual Subject Subject { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Fake> Fakes { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}

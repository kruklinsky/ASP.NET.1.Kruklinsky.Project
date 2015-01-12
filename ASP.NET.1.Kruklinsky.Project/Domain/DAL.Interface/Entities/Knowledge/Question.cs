using System.Collections.Generic;

namespace DAL.Interface.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int Level { get; set; }

        public string Topic { get; set; }

        public string Text { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DAL.Interface.Entities
{
    public class Test
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Name { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }


        public Lazy<IEnumerable<Question>> Questions { get; set; }
    }
}

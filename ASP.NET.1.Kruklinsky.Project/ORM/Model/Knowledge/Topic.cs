using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Model
{
    public class Topic
    {
        public int TopicId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descritpion { get; set; }

        public Subject Subject { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
    }
}

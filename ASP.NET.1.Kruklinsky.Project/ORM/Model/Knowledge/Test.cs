using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Model
{
    public class Test
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }

        public int SubjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Topic { get; set; }

        public string Description { get; set; }


        public virtual Subject Subject { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Model
{
    public class Question
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public string Text { get; set; }

        public string Description { get; set; }


        public virtual Subject Subject { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Fake> Fakes { get; set; }
    }
}

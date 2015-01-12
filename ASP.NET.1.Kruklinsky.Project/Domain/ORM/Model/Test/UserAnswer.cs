using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class UserAnswer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AnswerId { get; set; }

        public int QuestionId { get; set; }

        public int ResultId { get; set; }

        [Required]
        public bool IsRight { get; set; }


        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public virtual Result Result { get; set; }
    }
}

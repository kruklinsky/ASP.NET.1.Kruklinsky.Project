using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Model
{
    public class Result
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }

        public int TestId { get; set; }

        public Guid UserId { get; set; }

        public DateTime Start { get; set; }
        public TimeSpan Time { get; set; }


        [ForeignKey("TestId")]
        public virtual Test Test { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<UserAnswer> Answers { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Model
{
    public class Profile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual User User { get; set; }
    }
}
